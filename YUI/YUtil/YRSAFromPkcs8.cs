using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace YUI.WPF.YUtil
{
    /// <summary>
    /// RSA加密解密类
    /// </summary>
    public static class YRSAFromPkcs8
    {
        /// <summary>
        /// RSA最大解密密文大小
        /// 注意：这个和密钥长度有关系, 公式= 密钥长度 / 8
        /// </summary>
        private const int MaxDecryptBlock = 128;

        /// <summary>
        /// RSA最大加密密文大小
        /// </summary>
        private const int MaxEncryptBlock = 117;

        /// <summary>    
        /// 签名    
        /// </summary>    
        /// <param name="content">待签名字符串</param>    
        /// <param name="privateKey">私钥</param>    
        /// <param name="inputCharset">编码格式</param>    
        /// <returns>签名后字符串</returns>    
        public static string Sign(string content, string privateKey, string inputCharset)
        {
            var data = Encoding.GetEncoding(inputCharset).GetBytes(content);
            var rsa = DecodePemPrivateKey(privateKey);
            SHA1 sh = new SHA1CryptoServiceProvider();
            var signData = rsa.SignData(data, sh);
            return Convert.ToBase64String(signData);
        }

        /// <summary>    
        /// 验签    
        /// </summary>    
        /// <param name="content">待验签字符串</param>    
        /// <param name="signedString">签名</param>    
        /// <param name="publicKey">公钥</param>    
        /// <param name="inputCharset">编码格式</param>    
        /// <returns>true(通过)，false(不通过)</returns>    
        public static bool Verify(string content, string signedString, string publicKey, string inputCharset)
        {
            var data = Encoding.GetEncoding(inputCharset).GetBytes(content);
            var base64 = Convert.FromBase64String(signedString);
            var paraPub = ConvertFromPublicKey(publicKey);
            var rsaPub = new RSACryptoServiceProvider();
            rsaPub.ImportParameters(paraPub);
            SHA1 sh = new SHA1CryptoServiceProvider();
            var result = rsaPub.VerifyData(data, sh, base64);
            return result;
        }

        /// <summary>    
        /// 加密    
        /// </summary>    
        /// <param name="resData">需要加密的字符串</param>    
        /// <param name="publicKey">公钥</param>    
        /// <param name="inputCharset">编码格式</param>    
        /// <returns>密文</returns>    
        public static string EncryptData(string resData, string publicKey, string inputCharset)
        {
            var dataToEncrypt = Encoding.GetEncoding(inputCharset).GetBytes(resData);
            var result = EncryptToString(dataToEncrypt, publicKey);
            return result;
        }

        /// <summary>    
        /// 加密    
        /// </summary>    
        /// <param name="data">需要加密的字符串</param>    
        /// <param name="publicKey">公钥</param>
        /// <returns>密文</returns>    
        public static byte[] EncryptData(byte[] data, string publicKey)
        {
            var rsa = DecodePemPublicKey(publicKey);
            using (var stream = new MemoryStream(data.Length))
            {
                var buf = new byte[MaxEncryptBlock];
                for (var j = 0; j < data.Length / MaxEncryptBlock; j++)
                {
                    Array.Copy(data, MaxEncryptBlock * j, buf, 0, MaxEncryptBlock);
                    var result = rsa.Encrypt(buf, false);
                    stream.Write(result, 0, result.Length);
                }

                var remainder = data.Length % MaxEncryptBlock;
                if (remainder > 0)
                {
                    buf = new byte[remainder];
                    Array.Copy(data, MaxEncryptBlock * (data.Length / MaxEncryptBlock), buf, 0, remainder);
                    var result = rsa.Encrypt(buf, false);
                    stream.Write(result, 0, result.Length);
                }

                return stream.ToArray();
            }
        }

        /// <summary>    
        /// 解密    
        /// </summary>    
        /// <param name="resData">加密字符串</param>    
        /// <param name="privateKey">私钥</param>    
        /// <param name="inputCharset">编码格式</param>    
        /// <returns>明文</returns>    
        public static string DecryptData(string resData, string privateKey, string inputCharset)
        {
            var dataToDecrypt = Convert.FromBase64String(resData);
            var result = "";
            for (var j = 0; j < dataToDecrypt.Length / MaxDecryptBlock; j++)
            {
                var buf = new byte[MaxDecryptBlock];
                for (var i = 0; i < MaxDecryptBlock; i++)
                {

                    buf[i] = dataToDecrypt[i + MaxDecryptBlock * j];
                }
                result += Decrypt(buf, privateKey, inputCharset);
            }
            return result;
        }

        /// <summary>    
        /// 解密    
        /// </summary>    
        /// <param name="data">加密的数据</param>    
        /// <param name="privateKey">私钥</param>    
        /// <returns>解密后的数据</returns>    
        public static byte[] DecryptData(byte[] data, string privateKey)
        {
            var rsa = DecodePemPrivateKey(privateKey);
            using (var stream = new MemoryStream(data.Length))
            {
                var buf = new byte[MaxDecryptBlock];
                for (var j = 0; j < data.Length / MaxDecryptBlock; j++)
                {
                    Array.Copy(data, MaxDecryptBlock * j, buf, 0, MaxDecryptBlock);
                    var result = rsa.Decrypt(buf, false);
                    stream.Write(result, 0, result.Length);
                }
                return stream.ToArray();
            }
        }

        #region 内部方法    

        private static byte[] Encrypt(byte[] data, string publicKey)
        {
            var rsa = DecodePemPublicKey(publicKey);
            return rsa.Encrypt(data, false);
        }

        private static string EncryptToString(byte[] data, string publicKey)
        {
            var result = Encrypt(data, publicKey);
            return Convert.ToBase64String(result);
        }

        private static byte[] Decrypt(byte[] data, string privateKey)
        {
            var rsa = DecodePemPrivateKey(privateKey);
            return rsa.Decrypt(data, false);
        }

        private static string Decrypt(byte[] data, string privateKey, string inputCharset)
        {
            var source = Decrypt(data, privateKey);
            var asciiChars = new char[Encoding.GetEncoding(inputCharset).GetCharCount(source, 0, source.Length)];
            Encoding.GetEncoding(inputCharset).GetChars(source, 0, source.Length, asciiChars, 0);
            var result = new string(asciiChars);
            return result;
        }

        private static RSACryptoServiceProvider DecodePemPublicKey(string pem)
        {
            var pkcs8PublicKey = Convert.FromBase64String(pem);
            var rsa = DecodeRsaPublicKey(pkcs8PublicKey);
            return rsa;
        }

        private static RSACryptoServiceProvider DecodePemPrivateKey(string pem)
        {
            var pkcs8PrivateKey = Convert.FromBase64String(pem);
            var rsa = DecodePrivateKeyInfo(pkcs8PrivateKey);
            return rsa;
        }

        private static RSACryptoServiceProvider DecodePrivateKeyInfo(byte[] pkcs8)
        {
            byte[] seqOid = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };

            var mem = new MemoryStream(pkcs8);
            var lenStream = (int)mem.Length;
            var binaryReader = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading    

            try
            {
                var twoBytes = binaryReader.ReadUInt16();
                if (twoBytes == 0x8130)         //data read as little endian order (actual data order for Sequence is 30 81)    
                    binaryReader.ReadByte();    //advance 1 byte    
                else if (twoBytes == 0x8230)
                    binaryReader.ReadInt16();   //advance 2 bytes    
                else
                    return null;

                var bt = binaryReader.ReadByte();
                if (bt != 0x02)
                    return null;

                twoBytes = binaryReader.ReadUInt16();

                if (twoBytes != 0x0001)
                    return null;

                var seq = binaryReader.ReadBytes(15);
                if (!CompareByteArrays(seq, seqOid))    //make sure Sequence for OID is correct    
                    return null;

                bt = binaryReader.ReadByte();
                if (bt != 0x04)    //expect an Octet string    
                    return null;

                bt = binaryReader.ReadByte();        //read next byte, or next 2 bytes is  0x81 or 0x82; otherwise bt is the byte count    
                if (bt == 0x81)
                    binaryReader.ReadByte();
                else
                    if (bt == 0x82)
                    binaryReader.ReadUInt16();
                //------ at this stage, the remaining sequence should be the RSA private key    

                var rsaPrivateKey = binaryReader.ReadBytes((int)(lenStream - mem.Position));
                return DecodeRsaPrivateKey(rsaPrivateKey);
            }

            catch (Exception)
            {
                return null;
            }

            finally { binaryReader.Close(); }

        }

        private static bool CompareByteArrays(IReadOnlyCollection<byte> a, IReadOnlyList<byte> b)
        {
            if (a.Count != b.Count)
                return false;
            var i = 0;
            foreach (var c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        private static RSACryptoServiceProvider DecodeRsaPublicKey(byte[] publicKey)
        {
            // encoded OID sequence for PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"    
            byte[] seqOid = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------    
            var mem = new MemoryStream(publicKey);
            var binaryReader = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading    

            try
            {

                var twoBytes = binaryReader.ReadUInt16();
                if (twoBytes == 0x8130)         //data read as little endian order (actual data order for Sequence is 30 81)    
                    binaryReader.ReadByte();    //advance 1 byte    
                else if (twoBytes == 0x8230)
                    binaryReader.ReadInt16();   //advance 2 bytes    
                else
                    return null;

                var seq = binaryReader.ReadBytes(15);
                if (!CompareByteArrays(seq, seqOid))    //make sure Sequence for OID is correct    
                    return null;

                twoBytes = binaryReader.ReadUInt16();
                if (twoBytes == 0x8103)         //data read as little endian order (actual data order for Bit String is 03 81)    
                    binaryReader.ReadByte();    //advance 1 byte    
                else if (twoBytes == 0x8203)
                    binaryReader.ReadInt16();   //advance 2 bytes    
                else
                    return null;

                var bt = binaryReader.ReadByte();
                if (bt != 0x00)                 //expect null byte next    
                    return null;

                twoBytes = binaryReader.ReadUInt16();
                if (twoBytes == 0x8130)         //data read as little endian order (actual data order for Sequence is 30 81)    
                    binaryReader.ReadByte();    //advance 1 byte    
                else if (twoBytes == 0x8230)
                    binaryReader.ReadInt16();   //advance 2 bytes    
                else
                    return null;

                twoBytes = binaryReader.ReadUInt16();
                byte lowByte;
                byte highByte = 0x00;

                if (twoBytes == 0x8102)                 //data read as little endian order (actual data order for Integer is 02 81)    
                    lowByte = binaryReader.ReadByte();  // read next bytes which is bytes in modulus    
                else if (twoBytes == 0x8202)
                {
                    highByte = binaryReader.ReadByte(); //advance 2 bytes    
                    lowByte = binaryReader.ReadByte();
                }
                else
                    return null;
                byte[] modInt = { lowByte, highByte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order    
                var modSize = BitConverter.ToInt32(modInt, 0);

                var firstByte = binaryReader.ReadByte();
                binaryReader.BaseStream.Seek(-1, SeekOrigin.Current);

                if (firstByte == 0x00)
                {   //if first byte (highest order) of modulus is zero, don't include it    
                    binaryReader.ReadByte();    //skip this null byte    
                    modSize -= 1;               //reduce modulus buffer size by 1    
                }

                var modulus = binaryReader.ReadBytes(modSize);   //read the modulus bytes    

                if (binaryReader.ReadByte() != 0x02)            //expect an Integer for the exponent data    
                    return null;
                var expBytes = (int)binaryReader.ReadByte();        // should only need one byte for actual exponent data (for all useful values)    
                var exponent = binaryReader.ReadBytes(expBytes);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----    
                var rsa = new RSACryptoServiceProvider();
                var rsaKeyInfo = new RSAParameters
                {
                    Modulus = modulus,
                    Exponent = exponent
                };
                rsa.ImportParameters(rsaKeyInfo);
                return rsa;
            }
            catch (Exception)
            {
                return null;
            }

            finally { binaryReader.Close(); }

        }

        private static RSACryptoServiceProvider DecodeRsaPrivateKey(byte[] privateKey)
        {
            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

            // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------    
            var mem = new MemoryStream(privateKey);
            var binaryReader = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading    
            try
            {
                var twoBytes = binaryReader.ReadUInt16();
                if (twoBytes == 0x8130)    //data read as little endian order (actual data order for Sequence is 30 81)    
                    binaryReader.ReadByte();    //advance 1 byte    
                else if (twoBytes == 0x8230)
                    binaryReader.ReadInt16();    //advance 2 bytes    
                else
                    return null;

                twoBytes = binaryReader.ReadUInt16();
                if (twoBytes != 0x0102)    //version number    
                    return null;
                var bt = binaryReader.ReadByte();
                if (bt != 0x00)
                    return null;

                //------  all private key components are Integer sequences ----    
                var elems = GetIntegerSize(binaryReader);
                MODULUS = binaryReader.ReadBytes(elems);

                elems = GetIntegerSize(binaryReader);
                E = binaryReader.ReadBytes(elems);

                elems = GetIntegerSize(binaryReader);
                D = binaryReader.ReadBytes(elems);

                elems = GetIntegerSize(binaryReader);
                P = binaryReader.ReadBytes(elems);

                elems = GetIntegerSize(binaryReader);
                Q = binaryReader.ReadBytes(elems);

                elems = GetIntegerSize(binaryReader);
                DP = binaryReader.ReadBytes(elems);

                elems = GetIntegerSize(binaryReader);
                DQ = binaryReader.ReadBytes(elems);

                elems = GetIntegerSize(binaryReader);
                IQ = binaryReader.ReadBytes(elems);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----    
                var rsa = new RSACryptoServiceProvider();
                var rsaParams = new RSAParameters
                {
                    Modulus = MODULUS,
                    Exponent = E,
                    D = D,
                    P = P,
                    Q = Q,
                    DP = DP,
                    DQ = DQ,
                    InverseQ = IQ
                };
                rsa.ImportParameters(rsaParams);
                return rsa;
            }
            catch (Exception)
            {
                return null;
            }
            finally { binaryReader.Close(); }
        }

        private static int GetIntegerSize(BinaryReader binaryReader)
        {
            int count;
            var bt = binaryReader.ReadByte();
            if (bt != 0x02)        //expect integer    
                return 0;
            bt = binaryReader.ReadByte();

            if (bt == 0x81)
                count = binaryReader.ReadByte();    // data size in next byte    
            else if (bt == 0x82)
            {
                var highByte = binaryReader.ReadByte();
                var lowByte = binaryReader.ReadByte();
                byte[] modInt = { lowByte, highByte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modInt, 0);
            }
            else
            {
                count = bt;        // we already have the data size    
            }



            while (binaryReader.ReadByte() == 0x00)
            {    //remove high order zeros in data    
                count -= 1;
            }
            binaryReader.BaseStream.Seek(-1, SeekOrigin.Current);        //last ReadByte wasn't a removed zero, so back up a byte    
            return count;
        }

        #endregion

        #region 解析.net 生成的Pem    
        private static RSAParameters ConvertFromPublicKey(string pemFileContent)
        {

            if (string.IsNullOrEmpty(pemFileContent))
            {
                throw new ArgumentNullException(nameof(pemFileContent), @"This arg can't be empty.");
            }
            pemFileContent = pemFileContent.Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "").Replace("\n", "").Replace("\r", "");
            var keyData = Convert.FromBase64String(pemFileContent);
            var keySize1024 = (keyData.Length == 162);
            var keySize2048 = (keyData.Length == 294);
            if (!(keySize1024 || keySize2048))
            {
                throw new ArgumentException("pem file content is incorrect, Only support the key size is 1024 or 2048");
            }
            var pemModulus = (keySize1024 ? new byte[128] : new byte[256]);
            var pemPublicExponent = new byte[3];
            Array.Copy(keyData, (keySize1024 ? 29 : 33), pemModulus, 0, (keySize1024 ? 128 : 256));
            Array.Copy(keyData, (keySize1024 ? 159 : 291), pemPublicExponent, 0, 3);
            var para = new RSAParameters { Modulus = pemModulus, Exponent = pemPublicExponent };
            return para;
        }

        /// <summary>  
        /// 将pem格式私钥(1024 or 2048)转换为RSAParameters  
        /// </summary>  
        /// <param name="pemFileContent">pem私钥内容</param>  
        /// <returns>转换得到的RSAParameters</returns>  
        // ReSharper disable once UnusedMember.Local
        private static RSAParameters ConvertFromPrivateKey(string pemFileContent)
        {
            if (string.IsNullOrEmpty(pemFileContent))
            {
                throw new ArgumentNullException(nameof(pemFileContent), @"This arg can't be empty.");
            }
            pemFileContent = pemFileContent.Replace("-----BEGIN RSA PRIVATE KEY-----", "").Replace("-----END RSA PRIVATE KEY-----", "").Replace("\n", "").Replace("\r", "");
            var keyData = Convert.FromBase64String(pemFileContent);

            var keySize1024 = (keyData.Length == 609 || keyData.Length == 610);
            var keySize2048 = (keyData.Length == 1190 || keyData.Length == 1192);

            if (!(keySize1024 || keySize2048))
            {
                throw new ArgumentException("pem file content is incorrect, Only support the key size is 1024 or 2048");
            }

            var index = (keySize1024 ? 11 : 12);
            var pemModulus = (keySize1024 ? new byte[128] : new byte[256]);
            Array.Copy(keyData, index, pemModulus, 0, pemModulus.Length);

            index += pemModulus.Length;
            index += 2;
            var pemPublicExponent = new byte[3];
            Array.Copy(keyData, index, pemPublicExponent, 0, 3);

            index += 3;
            index += 4;
            if (keyData[index] == 0)
            {
                index++;
            }
            var pemPrivateExponent = (keySize1024 ? new byte[128] : new byte[256]);
            Array.Copy(keyData, index, pemPrivateExponent, 0, pemPrivateExponent.Length);

            index += pemPrivateExponent.Length;
            index += (keySize1024 ? ((int)keyData[index + 1] == 64 ? 2 : 3) : ((int)keyData[index + 2] == 128 ? 3 : 4));
            var pemPrime1 = (keySize1024 ? new byte[64] : new byte[128]);
            Array.Copy(keyData, index, pemPrime1, 0, pemPrime1.Length);

            index += pemPrime1.Length;
            index += (keySize1024 ? ((int)keyData[index + 1] == 64 ? 2 : 3) : ((int)keyData[index + 2] == 128 ? 3 : 4));
            var pemPrime2 = (keySize1024 ? new byte[64] : new byte[128]);
            Array.Copy(keyData, index, pemPrime2, 0, pemPrime2.Length);

            index += pemPrime2.Length;
            index += (keySize1024 ? ((int)keyData[index + 1] == 64 ? 2 : 3) : ((int)keyData[index + 2] == 128 ? 3 : 4));
            var pemExponent1 = (keySize1024 ? new byte[64] : new byte[128]);
            Array.Copy(keyData, index, pemExponent1, 0, pemExponent1.Length);

            index += pemExponent1.Length;
            index += (keySize1024 ? ((int)keyData[index + 1] == 64 ? 2 : 3) : ((int)keyData[index + 2] == 128 ? 3 : 4));
            var pemExponent2 = (keySize1024 ? new byte[64] : new byte[128]);
            Array.Copy(keyData, index, pemExponent2, 0, pemExponent2.Length);

            index += pemExponent2.Length;
            index += (keySize1024 ? ((int)keyData[index + 1] == 64 ? 2 : 3) : ((int)keyData[index + 2] == 128 ? 3 : 4));
            var pemCoefficient = (keySize1024 ? new byte[64] : new byte[128]);
            Array.Copy(keyData, index, pemCoefficient, 0, pemCoefficient.Length);

            var para = new RSAParameters
            {
                Modulus = pemModulus,
                Exponent = pemPublicExponent,
                D = pemPrivateExponent,
                P = pemPrime1,
                Q = pemPrime2,
                DP = pemExponent1,
                DQ = pemExponent2,
                InverseQ = pemCoefficient
            };
            return para;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ExtractFromPemFile(string filePath)
        {
            string result;
            var COMMENT_BEGIN_FLAG = "-----";
            //=============
            //从头到尾以流的方式读出文本文件, 该方法会一行一行读出文本
            using (var sr = new StreamReader(filePath, Encoding.UTF8))
            {
                string str;
                var sb = new StringBuilder();
                while ((str = sr.ReadLine()) != null)
                {
                    if (!str.StartsWith(COMMENT_BEGIN_FLAG))
                    {
                        sb.Append(str);
                    }
                }
                result = sb.ToString();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ExtractFromPemFormat(string input)
        {
            var COMMENT_BEGIN_FLAG = "-----";
            string[] splitFlagArray = { "\r", "\n" };
            var itemArray = input.Split(splitFlagArray, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            foreach (var item in itemArray)
            {
                if (!item.StartsWith(COMMENT_BEGIN_FLAG))
                {
                    sb.Append(item);
                }
            }
            var result = sb.ToString();
            return result;
        }
    }
}
