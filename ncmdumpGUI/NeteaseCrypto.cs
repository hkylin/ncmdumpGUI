using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ncmdumpGUI
{
    class NeteaseCrypto
    {
        private static byte[] _flag = new byte[8] { 0x43, 0x54, 0x45, 0x4e, 0x46, 0x44, 0x41, 0x4d };

        private static byte[] _coreBoxKey = new byte[16] { 0x68, 0x7A, 0x48, 0x52, 0x41, 0x6D, 0x73, 0x6F, 0x35, 0x6B, 0x49, 0x6E, 0x62, 0x61, 0x78, 0x57 };
        private static byte[] _modifyBoxKey = new byte[16] { 0x23, 0x31, 0x34, 0x6C, 0x6A, 0x6B, 0x5F, 0x21, 0x5C, 0x5D, 0x26, 0x30, 0x55, 0x3C, 0x27, 0x28 };

        private NeteaseCopyrightData _cdata = null;

        private Bitmap _cover = null;
        public Bitmap Cover { get => _cover; }

        private double _progress;
        public double Progress { get => _progress; }

        private byte[] _keyBox;

        private FileStream _file;
        private FileInfo _fileInfo;

        public NeteaseCrypto(FileInfo fileInfo)
        {
            _fileInfo = fileInfo;
            _file = _fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read);

            byte[] flag = new byte[8];
            _file.Read(flag, 0, flag.Length);

            if (!flag.SequenceEqual(_flag))
            {
                throw new Exception(_file.Name + "不是一个有效的ncm文件！");
            }

            // not use less
            _file.Seek(2, SeekOrigin.Current);

            byte[] coreKeyChunk = ReadChunk(_file);
            for (int i = 0; i < coreKeyChunk.Length; i++)
            {
                coreKeyChunk[i] ^= 0x64;
            }
            int ckcLen = AesDecrypt(coreKeyChunk, _coreBoxKey);

            byte[] finalKey = new byte[ckcLen - 17];
            Array.Copy(coreKeyChunk, 17, finalKey, 0, finalKey.Length);

            _keyBox = new byte[256];
            for (int i = 0; i < _keyBox.Length; i++)
            {
                _keyBox[i] = (byte)i;
            }

            byte swap = 0;
            byte c = 0;
            byte last_byte = 0;
            byte key_offset = 0;

            for (int i = 0; i < _keyBox.Length; i++)
            {
                swap = _keyBox[i];
                c = (byte)((swap + last_byte + finalKey[key_offset++]) & 0xff);
                if (key_offset >= finalKey.Length) key_offset = 0;
                _keyBox[i] = _keyBox[c];
                _keyBox[c] = swap;
                last_byte = c;
            }

            byte[] dontModifyChunk = ReadChunk(_file);
            int startIndex = 0;
            for (int i = 0; i < dontModifyChunk.Length; i++)
            {
                dontModifyChunk[i] ^= 0x63;
                if (dontModifyChunk[i] == 58 && startIndex == 0)
                {
                    startIndex = i + 1;
                }
            }

            byte[] dontModifyDecryptChunk = Convert.FromBase64String(Encoding.UTF8.GetString(dontModifyChunk, startIndex, dontModifyChunk.Length - startIndex));
            int mdcLen = AesDecrypt(dontModifyDecryptChunk, _modifyBoxKey);

            DataContractJsonSerializer d = new DataContractJsonSerializer(typeof(NeteaseCopyrightData));
            // skip `music:`
            using (MemoryStream reader = new MemoryStream(dontModifyDecryptChunk, 6, mdcLen - 6))
            {
                _cdata = d.ReadObject(reader) as NeteaseCopyrightData;
            }

            // skip crc & some use less chunk
            _file.Seek(9, SeekOrigin.Current);

            byte[] imageChunk = ReadChunk(_file);
            using (MemoryStream imageStream = new MemoryStream(imageChunk))
            {
                _cover = Image.FromStream(imageStream) as Bitmap;
            }
        }

        private byte[] ReadChunk(FileStream fs)
        {
            uint len = fs.ReadUInt32();
            byte[] chunk = new byte[len];

            // unsafe
            fs.Read(chunk, 0, (int)len);

            return chunk;
        }

        private int AesDecrypt(byte[] data, byte[] key)
        {
            var aes = Aes.Create();
            aes.Mode = CipherMode.ECB;
            aes.Key = key;
            aes.Padding = PaddingMode.PKCS7;

            using (MemoryStream stream = new MemoryStream(data))
            {
                using (CryptoStream cs = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    return cs.Read(data, 0, data.Length);
                }
            }
        }

        public void Dump(string destDir)
        {
            int n = 0x8000;
            double totalLen = _file.Length - _file.Position;
            double alreadyProcess = 0;

            // 将.ncm替换成最终转换的文件后缀名
            string destFileName = string.Format("{0}.{1}", _fileInfo.Name.Substring(0, _fileInfo.Name.Length - 4), this._cdata.Format);
            string destFilePath = Path.Combine(destDir, destFileName);

            using (FileStream stream = new FileStream(destFilePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                while (n > 1)
                {
                    byte[] chunk = new byte[n];
                    n = _file.Read(chunk, 0, n);

                    for (int i = 0; i < n; i++)
                    {
                        int j = (i + 1) & 0xff;
                        chunk[i] ^= _keyBox[(_keyBox[j] + _keyBox[(_keyBox[j] + j) & 0xff]) & 0xff];
                    }

                    stream.Write(chunk, 0, n);

                    alreadyProcess += n;

                    _progress = (alreadyProcess / totalLen) * 100d;
                }
            }
        }
    }
}
