using System;
using System.Collections.Generic;
using System.Text;

namespace Ecm.Domain.Ware
{
    public class Ware_Phieuchi_Congno_Chititet
    {
        private object id_phieuchi_congno_chitiet;
private object id_phieuchi_congno;
private object chungtu_goc;
private object sotien;
private object sotien_thanhtoan;
private object sotien_no;
private object sotien_thanhtoan_trongky;
private object sotien_no_trongky;

        [System.Xml.Serialization.XmlElement][System.Runtime.Serialization.DataMemberAttribute]
        public object Id_Phieuchi_Congno_Chitiet
        {
            set { id_phieuchi_congno_chitiet = value; }
            get { return id_phieuchi_congno_chitiet; }
        }

        [System.Xml.Serialization.XmlElement][System.Runtime.Serialization.DataMemberAttribute]
        public object Id_Phieuchi_Congno
        {
            set { id_phieuchi_congno = value; }
            get { return id_phieuchi_congno; }
        }

        [System.Xml.Serialization.XmlElement][System.Runtime.Serialization.DataMemberAttribute]
        public object Chungtu_Goc
        {
            set { chungtu_goc = value; }
            get { return chungtu_goc; }
        }

        [System.Xml.Serialization.XmlElement][System.Runtime.Serialization.DataMemberAttribute]
        public object Sotien
        {
            set { sotien = value; }
            get { return sotien; }
        }

        [System.Xml.Serialization.XmlElement][System.Runtime.Serialization.DataMemberAttribute]
        public object Sotien_Thanhtoan
        {
            set { sotien_thanhtoan = value; }
            get { return sotien_thanhtoan; }
        }

        [System.Xml.Serialization.XmlElement][System.Runtime.Serialization.DataMemberAttribute]
        public object Sotien_No
        {
            set { sotien_no = value; }
            get { return sotien_no; }
        }

        [System.Xml.Serialization.XmlElement][System.Runtime.Serialization.DataMemberAttribute]
        public object Sotien_Thanhtoan_Trongky
        {
            set { sotien_thanhtoan_trongky = value; }
            get { return sotien_thanhtoan_trongky; }
        }

        [System.Xml.Serialization.XmlElement][System.Runtime.Serialization.DataMemberAttribute]
        public object Sotien_No_Trongky
        {
            set { sotien_no_trongky = value; }
            get { return sotien_no_trongky; }
        }
    }
}
