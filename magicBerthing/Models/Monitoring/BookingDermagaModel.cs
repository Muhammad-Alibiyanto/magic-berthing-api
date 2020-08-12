using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.Models.Monitoring
{
    public class BookingDermagaModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public int count { get; set; }
        public List<GetBookingDermagaList> data { get; set; }
    }

    public class BookingDermagaHeader
    {
        public string booking_id { get; set; }
        public string kode_kapal { get; set; }
        public string nama_kapal { get; set; }
        public string gt { get; set; }
        public string loa { get; set; }
        public string draft { get; set; }
        public string komoditi { get; set; }
        public string kd_cabang { get; set; }
        public DateTime created_date { get; set; }
        public int lama_rencana_tambat { get; set; }
        public string kode_pelabuhan { get; set; }
        public string nama_pelabuhan { get; set; }
        public int kade_awal { get; set; }
        public int kade_akhir { get; set; }
        public string kode_pelanggan { get; set; }
        public List<BookingDermagaDetail> dermaga_detail { get; set; }

    }

    public class BookingDermagaDetail
    {
        public string booking_id { get; set; }
        public string jasa { get; set; }
        public string asal { get; set; }
        public string tujuan { get; set; }
        public DateTime tgl_mulai { get; set; }
        public DateTime tgl_selesai { get; set; }
        public string jam_mulai { get; set; }
        public string jam_selesai { get; set; }
        public int jumlah_gerakan { get; set; }
        public string gerakan { get; set; }
        public float epb { get; set; }
        public DateTime created_date { get; set; }
        public string kode_pelabuhan_asal { get; set; }
        public string kode_pelabuhan_tujuan { get; set; }
    }

    public class GetBookingDermagaList
    {
        public string booking_id { get; set; }
        public string kode_kapal { get; set; }
        public string nama_kapal { get; set; }
        public string gt { get; set; }
        public string loa { get; set; }
        public string draft { get; set; }
        public string komoditi { get; set; }
        public string kd_cabang { get; set; }
        public DateTime created_date { get; set; }
        public int lama_rencana_tambat { get; set; }
        public string kode_pelabuhan { get; set; }
        public string nama_pelabuhan { get; set; }
        public int kade_awal { get; set; }
        public int kade_akhir { get; set; }
        public string kode_pelanggan { get; set; }
        public string jasa { get; set; }
        public string asal { get; set; }
        public string tujuan { get; set; }
        public DateTime tgl_mulai { get; set; }
        public DateTime tgl_selesai { get; set; }
        public string jam_mulai { get; set; }
        public string jam_selesai { get; set; }
        public int jumlah_gerakan { get; set; }
        public string gerakan { get; set; }
        public float epb { get; set; }
        public string kode_pelabuhan_asal { get; set; }
        public string kode_pelabuhan_tujuan { get; set; }
    }

    public class BookingDermagaParam
    {
        public string kode_booking { get; set; }
        public string kode_pelanggan { get; set; }
    }
}
