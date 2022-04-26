using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace G_B.mesClasses
{
    public class Carduino
    {
        SerialPort oPort = null;

        public Carduino()
        {
            InitConnexion();
        }

        private void InitConnexion()
        {
            this.oPort = new SerialPort("COM3", 9600);
        }

        public void OpeConnexion()
        {
            this.oPort.Open();
        }

        public string ReadData()
        {
            return null;
        }

        public void CloseConnexion()
        {
            this.oPort.Close();
        }
    }
}
