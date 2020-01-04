using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace System.Cryptography
{
    public class RC2Crypt
    {
        public void Sample()
        {
            byte[] originalBytes = ASCIIEncoding.ASCII.GetBytes("Here is some data.");

            //Create a new RC2CryptoServiceProvider.
            RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();

            rc2CSP.UseSalt = true;

            rc2CSP.GenerateKey();
            rc2CSP.GenerateIV();

            rc2CSP.Key = ASCIIEncoding.ASCII.GetBytes("WIDLWEST");
            rc2CSP.IV = ASCIIEncoding.ASCII.GetBytes("WIDLWEST");

            //Encrypt the data.
            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, rc2CSP.CreateEncryptor(rc2CSP.Key, rc2CSP.IV), CryptoStreamMode.Write);

            //Write all data to the crypto stream and flush it.
            csEncrypt.Write(originalBytes, 0, originalBytes.Length);
            csEncrypt.FlushFinalBlock();

            //Get encrypted array of bytes.
            byte[] encryptedBytes = msEncrypt.ToArray();

            //Decrypt the previously encrypted message.
            MemoryStream msDecrypt = new MemoryStream(encryptedBytes);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, rc2CSP.CreateDecryptor(rc2CSP.Key, rc2CSP.IV), CryptoStreamMode.Read);

            byte[] unencryptedBytes = new byte[originalBytes.Length];

            //Read the data out of the crypto stream.
            csDecrypt.Read(unencryptedBytes, 0, unencryptedBytes.Length);

            //Convert the byte array back into a string.
            string plaintext = ASCIIEncoding.ASCII.GetString(unencryptedBytes);

            //Display the results.
                  //System.Windows.Forms.MessageBox.Show("Unencrypted text: " + plaintext);

            //   Console.ReadLine();

        }

        public String Decrypt(String text)
        {
            // byte[] originalBytes = ASCIIEncoding.ASCII.GetBytes(text);

            //Create a new RC2CryptoServiceProvider.
            if (text == null || text == "")
                return "";
            RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();

            rc2CSP.Key = ASCIIEncoding.ASCII.GetBytes("WIDLWEST");
            rc2CSP.IV = ASCIIEncoding.ASCII.GetBytes("WIDLWEST");

            byte[] encryptedBytes = GetBytes(text);

            //Decrypt the previously encrypted message.
            MemoryStream msDecrypt = new MemoryStream(encryptedBytes);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, rc2CSP.CreateDecryptor(rc2CSP.Key, rc2CSP.IV), CryptoStreamMode.Read);

            byte[] unencryptedBytes = new byte[encryptedBytes.Length];

            //Read the data out of the crypto stream.
            csDecrypt.Read(unencryptedBytes, 0, unencryptedBytes.Length);

            //Convert the byte array back into a string.
            string plaintext = ASCIIEncoding.ASCII.GetString(unencryptedBytes);

            //Display the results.
            //System.Windows.Forms.MessageBox.Show("Unencrypted text: " + plaintext);
            if (plaintext.Contains("?") && plaintext.LastIndexOf("?") != plaintext.Length - 1)
                plaintext = plaintext.Replace("?", "ñ");
            return plaintext;

            //   Console.ReadLine();

        }

        public String Encrypt(String text)
        {
            if (text == null)
                text = "";
            byte[] originalBytes = ASCIIEncoding.ASCII.GetBytes(text);

            //Create a new RC2CryptoServiceProvider.
            RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();

            rc2CSP.UseSalt = true;

            rc2CSP.GenerateKey();
            rc2CSP.GenerateIV();

            rc2CSP.Key = ASCIIEncoding.ASCII.GetBytes("WIDLWEST");
            rc2CSP.IV = ASCIIEncoding.ASCII.GetBytes("WIDLWEST");

            //Encrypt the data.
            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, rc2CSP.CreateEncryptor(rc2CSP.Key, rc2CSP.IV), CryptoStreamMode.Write);

            //Write all data to the crypto stream and flush it.
            csEncrypt.Write(originalBytes, 0, originalBytes.Length);
            csEncrypt.FlushFinalBlock();

            //Get encrypted array of bytes.
            byte[] encryptedBytes = msEncrypt.ToArray();

            return GetString(encryptedBytes);

        }

        private String GetString(byte[] sourceBytes)
        {
            String rst = "";

            for (int w = 0; w < sourceBytes.Length; w++)
            {
                rst = rst.Insert(rst.Length, System.Convert.ToString(sourceBytes[w]));
                if (w != sourceBytes.Length - 1)
                {
                    rst = rst.Insert(rst.Length, "-");
                }
            }

            return rst;
        }

        private byte[] GetBytes(String sourceString)
        {


            String[] aux = sourceString.Split('-');
            byte[] rstBytes = new byte[aux.Length];

            for (int w = 0; w < aux.Length; w++)
            {
                if (aux[w] != "")
                    rstBytes[w] = System.Convert.ToByte(aux[w]);

            }

            return rstBytes;
        }
    }
    public class Encripter
    {
        public Encripter()
        {

        }
        //  Call this function to remove the key from memory after use for security
        [DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern bool ZeroMemory(IntPtr Destination, int Length);

        // Function to Generate a 64 bits Key.
        public string GenerateKey()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();

            // Use the Automatically generated key for Encryption. 
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }

        public void EncryptFile(string sInputFilename,
           string sOutputFilename,
           string sKey)
        {
            FileStream fsInput = new FileStream(sInputFilename,
               FileMode.Open,
               FileAccess.Read);

            FileStream fsEncrypted = new FileStream(sOutputFilename,
               FileMode.Create,
               FileAccess.Write);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            CryptoStream cryptostream = new CryptoStream(fsEncrypted,
               desencrypt,
               CryptoStreamMode.Write);

            byte[] bytearrayinput = new byte[fsInput.Length];
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);

            cryptostream.Close();
            fsInput.Close();
            fsEncrypted.Close();
        }
        public void EncryptFile4(string Filename,
                    string sKey)
        {
            FileStream fsInput = new FileStream(Filename,
               FileMode.Open,
               FileAccess.Read);
            byte[] bytearrayinput = new byte[fsInput.Length];
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            fsInput.Close();

            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            FileStream fsEncrypted = new FileStream(Filename,
               FileMode.Create,
               FileAccess.Write);

            ICryptoTransform desencrypt = DES.CreateEncryptor();
            CryptoStream cryptostream = new CryptoStream(fsEncrypted,
               desencrypt,
               CryptoStreamMode.Write);


            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);

            cryptostream.Close();

            fsEncrypted.Close();
        }
        public void EncryptFile2(string data,
          string sOutputFilename,
          string sKey)
        {


            FileStream fsCreate = new FileStream("temp_key.key",
               FileMode.Create,
               FileAccess.ReadWrite);
            fsCreate.Close();
            StreamWriter sw = new StreamWriter("temp_key.key");
            sw.Write(data);
            sw.Close();




            FileStream fsInput = new FileStream("temp_key.key",
               FileMode.Open,
               FileAccess.ReadWrite);




            FileStream fsEncrypted = new FileStream(sOutputFilename,
               FileMode.Create,
               FileAccess.Write);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            if (sKey.Length > 8)
            {
                sKey = sKey.Remove(8);
            }

            if (sKey.Length < 8)
            {
                while (sKey.Length < 8)
                    sKey = sKey + "W";

            }
            //int k = DES.KeySize;
            //byte[] originalBytes = ASCIIEncoding.ASCII.GetBytes(sKey+"WILDWEST");
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor(/*ASCIIEncoding.ASCII.GetBytes(sKey), ASCIIEncoding.ASCII.GetBytes(sKey)*/);
            CryptoStream cryptostream = new CryptoStream(fsEncrypted,
               desencrypt,
               CryptoStreamMode.Write);

            byte[] bytearrayinput = new byte[fsInput.Length];
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);

            cryptostream.Close();
            fsInput.Close();
            fsEncrypted.Close();


            FileInfo fi = new FileInfo("temp_key.key");
            fi.Delete();
        }

        public void DecryptFile(string sInputFilename,
           string sOutputFilename,
           string sKey)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.
            //Set secret key For DES algorithm.
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Set initialization vector.
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            //Create a file stream to read the encrypted file back.
            FileStream fsread = new FileStream(sInputFilename,
               FileMode.Open,
               FileAccess.Read);
            //Create a DES decryptor from the DES instance.
            ICryptoTransform desdecrypt = DES.CreateDecryptor();
            //Create crypto stream set to read and do a 
            //DES decryption transform on incoming bytes.
            CryptoStream cryptostreamDecr = new CryptoStream(fsread,
               desdecrypt,
               CryptoStreamMode.Read);
            //Print the contents of the decrypted file.
            StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
            fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
            fsDecrypted.Flush();
            fsDecrypted.Close();
        }
        public void DecryptFile4(string Filename,
           string sKey)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.
            //Set secret key For DES algorithm.
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Set initialization vector.
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            //Create a file stream to read the encrypted file back.
            FileStream fsread = new FileStream(Filename,
               FileMode.Open,
               FileAccess.Read);
            //Create a DES decryptor from the DES instance.
            ICryptoTransform desdecrypt = DES.CreateDecryptor();
            //Create crypto stream set to read and do a 
            //DES decryption transform on incoming bytes.
            CryptoStream cryptostreamDecr = new CryptoStream(fsread,
               desdecrypt,
               CryptoStreamMode.Read);
            //Print the contents of the decrypted file.


            String data = new StreamReader(cryptostreamDecr).ReadToEnd();
            fsread.Close();
            StreamWriter fsDecrypted = new StreamWriter(Filename);
            fsDecrypted.Write(data);
            fsDecrypted.Flush();
            fsDecrypted.Close();
        }

        public String EncryptText(string text,
         string sKey)
        {
            //FileStream fsInput = new FileStream(sInputFilename,
            //   FileMode.Open,
            //   FileAccess.Read);

            FileStream fsEncrypted = new FileStream("temp.tmp",
            FileMode.Create,
            FileAccess.Write);

            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();

            CryptoStream cryptostream = new CryptoStream(fsEncrypted,
               desencrypt,
               CryptoStreamMode.Write);

            byte[] bytearrayinput = new byte[text.Length];
            //fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            for (int w = 0; w < text.Length; w++)
            {
                bytearrayinput[w] = System.Convert.ToByte(text[w]);
            }
            // bytearrayinput = System.Convert.ToByte(text);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);


            // fsEncrypted.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Close();
            // fsInput.Close();
            fsEncrypted.Close();

            StreamReader sr = new StreamReader("temp.tmp");
            string rst = sr.ReadToEnd();
            sr.Close();
            FileInfo fi = new FileInfo("temp.tmp");
            fi.Delete();


            return rst;
        }


        public string DecryptFile2(string sInputFilename,
         string sKey)
        {

            try
            {
StreamReader sr2 = new StreamReader(sInputFilename, Encoding.Unicode);
            Stream eam = sr2.BaseStream;

            //byte[] mybuffer = new byte[eam.Length];
            //eam.Position = 0;
            //eam.Read(mybuffer,0,System.Convert.ToInt32(eam.Length-1));

            //Stream eam = sr2.BaseStream;

            char[] mybuffer = new char[eam.Length];
            eam.Position = 0;
            sr2.Read(mybuffer, 0, System.Convert.ToInt32(eam.Length));

            // string rst = "" ;
            sr2.Close();



            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.
            //Set secret key For DES algorithm.
            //  sKey = sKey + "EEF";
            while (sKey.Length > 8)
            {
                sKey = sKey.Remove(sKey.Length - 1);
            }
            while (sKey.Length < 8)
            {
                sKey = sKey + "W";
            }
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Set initialization vector.
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            //Create a file stream to read the encrypted file back.
            FileStream fsread = new FileStream(sInputFilename,
               FileMode.Open,
               FileAccess.Read);
            //Create a DES decryptor from the DES instance.
            ICryptoTransform desdecrypt = DES.CreateDecryptor();
            //Create crypto stream set to read and do a 
            //DES decryption transform on incoming bytes.
            CryptoStream cryptostreamDecr = new CryptoStream(fsread,
               desdecrypt,
               CryptoStreamMode.Read);
            //Print the contents of the decrypted file.
            //StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
            // fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
            //fsDecrypted.Flush();
            // fsDecrypted.Close();

            StreamReader sr = new StreamReader(cryptostreamDecr);
            string str = sr.ReadToEnd();
            sr.Close();
            return str;
            }
            catch
            {
                return "error";
            }
            
        }


        public string DecryptText(string text,
        string sKey)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.
            //Set secret key For DES algorithm.
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Set initialization vector.
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            //Create a file stream to read the encrypted file back.



            //FileStream fscreate = new FileStream("temp.tmp",
            //FileMode.Create,
            //FileAccess.Write);
            //fscreate.Close();
            //StreamWriter sr = new StreamWriter("temp.tmp", false, Encoding.Unicode);
            //sr.Write(text);
            //sr.Close();

            // DecryptFile("temp.tmp", "temp2.tmp", sKey);
            //   EncryptFile("temp.tmp", "temp2.tmp", sKey);


            string rettext = DecryptFile2("temp.tmp", sKey);

            //FileInfo fi = new FileInfo("temp.tmp");
            //fi.Delete();


            return rettext;
        }


        public GCHandle PinKey(string sSecretKey)
        {
            return GCHandle.Alloc(sSecretKey, GCHandleType.Pinned);
        }

        public void RemoveKey(GCHandle gch, int length)
        {
            // Remove the Key from memory. 
            ZeroMemory(gch.AddrOfPinnedObject(), length);
            gch.Free();
        }
        //static void Main()
        //{

        //}
    }
}