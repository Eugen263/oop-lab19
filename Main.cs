using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HardwareInformation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGetInfo_Click(object sender, EventArgs e)
        {
            OutputResult("Процесор:", GetHardwareInfo("Win32_Processor", "Name"));
            OutputResult("Виробник:", GetHardwareInfo("Win32_Processor", "Manufacturer"));
            OutputResult("Опис:", GetHardwareInfo("Win32_Processor", "Description"));
            OutputResult("", new List<string>()); // виводимо порожній рядок

            OutputResult("Відеокарта:", GetHardwareInfo("Win32_VideoController", "Name"));
            OutputResult("Видеопроцесор:", GetHardwareInfo("Win32_VideoController", "VideoProcessor"));
            OutputResult("Версія драйверу:", GetHardwareInfo("Win32_VideoController", "DriverVersion"));
            OutputResult("Об’єм пам’яти (в байтах):", GetHardwareInfo("Win32_VideoController", "AdapterRAM"));
            OutputResult("", new List<string>());

            OutputResult("Назва DVD:", GetHardwareInfo("Win32_CDROMDrive", "Name"));
            OutputResult("Буква DVD:", GetHardwareInfo("Win32_CDROMDrive", "Drive"));
            OutputResult("", new List<string>());

            OutputResult("Жорстикий диск:", GetHardwareInfo("Win32_DiskDrive", "Caption"));
            OutputResult("Об’єм (в байтах):", GetHardwareInfo("Win32_DiskDrive", "Size"));

            OutputResult("Материнська плата:", GetHardwareInfo("Win32_BaseBoard", "Product"));
            OutputResult("Виробник материнської плати:", GetHardwareInfo("Win32_BaseBoard", "Manufacturer"));
            OutputResult("Серійний номер материнської плати:", GetHardwareInfo("Win32_BaseBoard", "SerialNumber"));
            Console.WriteLine();

            OutputResult("Мережеві адаптери:", GetHardwareInfo("Win32_NetworkAdapter", "Name"));
            OutputResult("IP-адреса:", GetHardwareInfo("Win32_NetworkAdapterConfiguration", "IPAddress"));
            OutputResult("MAC-адреса:", GetHardwareInfo("Win32_NetworkAdapterConfiguration", "MACAddress"));
            Console.WriteLine();

            OutputResult("BIOS:", GetHardwareInfo("Win32_BIOS", "Caption"));
            OutputResult("Виробник BIOS:", GetHardwareInfo("Win32_BIOS", "Manufacturer"));
            OutputResult("Версія BIOS:", GetHardwareInfo("Win32_BIOS", "Version"));
        }

        private List<string> GetHardwareInfo(string WIN32_Class, string ClassItemField)
        {
            List<string> result = new List<string>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + WIN32_Class);
            try
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    result.Add(obj[ClassItemField].ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        private void OutputResult(string info, List<string> result)
        {
            if (info.Length > 0)
            {
                richTextBox1.AppendText(info + "\n");
            }
            if (result.Count > 0)
            {
                for (int i = 0; i < result.Count; ++i)
                {
                    richTextBox1.AppendText(result[i] + "\n");
                }
            }
        }
    }
}

