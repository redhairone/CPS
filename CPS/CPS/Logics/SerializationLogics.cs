using CPS.M;
using Microsoft.Win32;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace CPS.Logics
{
    class SerializationLogics
    {
        internal static void Serialize(DataCapsule capsule)
        {
            SaveFileDialog sfd = new SaveFileDialog { Title = "Zapisz wykres", Filter = "Binary files (*.bin)|*.bin" };
            sfd.ShowDialog();

            if (sfd.FileName != "")
            {
                IFormatter formatter = new BinaryFormatter();
                using (var stream = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, capsule);
                }
            }
            else
            {
                MessageBox.Show("Nie wybrałeś nazwy zapisywanego pliku. Operacja zapisu zostaje anulowana.", "BŁĄD", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        internal static DataCapsule Deserialize()
        {
            DataCapsule result;
            OpenFileDialog ofd = new OpenFileDialog { Title = "Wczytaj wykres", Filter = "Binary files (*.bin)|*.bin" };
            ofd.ShowDialog();

            if (ofd.FileName != "")
            {
                IFormatter formatter = new BinaryFormatter();
                using (var stream = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    result = (DataCapsule)formatter.Deserialize(stream);
                }
                return result;
            }
            else
            {
                MessageBox.Show("Nie wybrałeś nazwy wczytywanego pliku. Operacja wczytania zostaje anulowana.", "BŁĄD", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        internal static DataCapsule[] LoadFiles()
        {
            DataCapsule[] results = new DataCapsule[2];
            OpenFileDialog ofd = new OpenFileDialog { Title = "Wczytaj wykres", Filter = "Binary files (*.bin)|*.bin" };
            ofd.ShowDialog();

            if (ofd.FileName != "")
            {
                IFormatter formatter = new BinaryFormatter();
                using (var stream = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    results[0] = (DataCapsule)formatter.Deserialize(stream);
                }
            }
            else
            {
                MessageBox.Show("Nie wybrałeś nazwy wczytywanego pliku.", "BŁĄD", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            ofd.FileName = "";
            ofd.ShowDialog();
            if (ofd.FileName != "")
            {
                IFormatter formatter = new BinaryFormatter();
                using (var stream = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    results[1] = (DataCapsule)formatter.Deserialize(stream);
                }
            }
            else
            {
                MessageBox.Show("Nie wybrałeś nazwy wczytywanego pliku.", "BŁĄD", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            return results;
        }
    }
}
