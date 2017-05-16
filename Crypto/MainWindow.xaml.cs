using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;


namespace Crypto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private CryptoKey cryptokey = new CryptoKey();
        private void encrypt_Click(object sender, RoutedEventArgs e)
        {
            if (CryptoCheckBox.IsChecked == true)
            {
                string IN = input.Text;
                string OUT = cryptokey.encryption(IN);
                result.Clear();
                result.Text = OUT;
                input.Clear();
            }
            else
            {
                System.Windows.MessageBox.Show("Please insert cryptokey");
            }
            
            
        }
        private void decrypt_Click(object sender, RoutedEventArgs e)
        {
            if (CryptoCheckBox.IsChecked == true)
            {
                string IN = input.Text;
                string OUT = cryptokey.decryption(IN);
                result.Clear();
                result.Text = OUT;
                input.Clear();
            }
            else
            {
                System.Windows.MessageBox.Show("Please insert cryptokey");
            }
        }

        private void SetCrypto_Click(object sender, RoutedEventArgs e)
        {
            
            if (CryptoCheckBox.IsChecked != true)
            {
                string key = Keyentry.Text;
                cryptokey.SetCryptoKey(key);
                CryptoCheckBox.IsChecked = true;
                Keyentry.Clear();
            }
            else
            {
                DialogResult Ask = System.Windows.Forms.MessageBox.Show("Key is already installed! click OK to replace key. cancel to exit.", "cryptokey",MessageBoxButtons.OKCancel);
                if (Ask == System.Windows.Forms.DialogResult.OK)
                {
                    cryptokey.reset();
                    string key = Keyentry.Text;
                    cryptokey.SetCryptoKey(key);
                    Keyentry.Clear();
                                        
                }
            }
            
        }
    }
    public class CryptoKey 
    {
        private List<string> _Alphabet = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private List<int> _Offset = new List<int>();
        int Counter = 0;
        char[] _keychars;
        public void SetCryptoKey(string key)
        {
            
            var _keychars = key.ToCharArray();
            var _keycharsString = new string[_keychars.Length];
            
            for (int index = 0; index < _keychars.Length; index++)
            {
                _keycharsString[index] = _keychars[index].ToString();
                _keycharsString[index] = _keycharsString[index].ToUpper();
                int workIndex = _Alphabet.IndexOf(_keycharsString[index]);
                _Offset.Add(workIndex);

            }
         }
        public string encryption(string input)
        {
            
            var Words = input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            Counter = 0;
            for(int index=0; index < Words.Length; index++)
            {
                
                var CharLetters = Words[index].ToCharArray();
                string[] Letters = new string[CharLetters.Length];
                for (int index2=0; index2 < CharLetters.Length; index2++)
                {
                    Letters[index2] = encrypt(CharLetters[index2]);
                }
                Words[index] = string.Join("", Letters);
                CharLetters = null;
            }
            string output;
            return output = string.Join(" ", Words);
            Words = null;
            
            
        }
        public string decryption(string input)
        {
            var Words = input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            Counter = 0;
            for (int index = 0; index < Words.Length; index++)
            {

                var CharLetters = Words[index].ToCharArray();
                string[] Letters = new string[CharLetters.Length];
                for (int index2 = 0; index2 < CharLetters.Length; index2++)
                {
                    Letters[index2] = decrypt(CharLetters[index2]);
                }
                Words[index] = string.Join("", Letters);
                CharLetters = null; 
            }
            string output;
            return output = string.Join(" ", Words);
            Words = null;
        }
        //public string encrypt(string ToEncrypt)
        //{
        //    int IndexInAlphabet = _Alphabet.IndexOf(ToEncrypt.ToUpper());
        //    int encryptieIndex = IndexInAlphabet + _Offset[Counter];
        //    if (encryptieIndex > 25)
        //    {
        //        encryptieIndex = encryptieIndex - 26;
        //    }
        //    return _Alphabet[encryptieIndex];
        //    if (Counter >= _Offset.Count)
        //    {
        //        Counter = 0;
        //    }
        //    else { Counter++; }

        //}
        public string encrypt(char c)
        {
            string H = c.ToString();
            H = H.ToUpper();
            if (Counter >= _Offset.Count)
            {
                Counter = 0;
            }
            int IndexInAlphabet = _Alphabet.IndexOf(H);
            if (IndexInAlphabet < 0)
            {
                return c.ToString();
            }
            int encryptieIndex = IndexInAlphabet + _Offset[Counter];
            if (encryptieIndex > 25)
            {
                encryptieIndex = encryptieIndex - 26;
            }

            if (Counter < _Offset.Count) { Counter++; }
            return _Alphabet[encryptieIndex];
           
        }
        public string decrypt(char d)
        {
            string H = d.ToString();
            H = H.ToUpper();
            int encryptedIndex = _Alphabet.IndexOf(H);
            if (encryptedIndex < 0)
            {
                return d.ToString();
            }
            if (Counter >= _Offset.Count)
            {
                Counter = 0;
            }

            int NormalIndex = encryptedIndex - _Offset[Counter];
            if (NormalIndex < 0)
            {
                NormalIndex = NormalIndex + 26;
            }

            if (Counter < _Offset.Count) { Counter++; }
            return _Alphabet[NormalIndex];
        }
        public void reset()
        {
            _Offset = new List<int>();
            _keychars = null;
            Counter = 0;
        }

    }
}
