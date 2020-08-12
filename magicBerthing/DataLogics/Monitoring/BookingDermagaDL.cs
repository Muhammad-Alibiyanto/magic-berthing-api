using Dapper;
using magicBerthing.Models.Monitoring;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace magicBerthing.DataLogics.Monitoring
{
    public class BookingDermagaDL
    {
        public Dictionary<String, String> createBooking(BookingDermagaHeader DermagaHeader)
        {
            Dictionary<String, String> result = new Dictionary<string, string>();

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {
                    var transaction = connection.BeginTransaction();

                    string header = @"INSERT INTO MAGIC_BOOKING_HEADER VALUES('" +
                                    DermagaHeader.booking_id + "', '" +
                                    DermagaHeader.kode_kapal + "', '" +
                                    DermagaHeader.nama_kapal + "', '" +
                                    DermagaHeader.gt + "', '" +
                                    DermagaHeader.loa + "', '" +
                                    DermagaHeader.draft + "', '" +
                                    DermagaHeader.komoditi + "', '" +
                                    DermagaHeader.kd_cabang + "', TO_DATE('" +
                                    DateTime.Now + "', 'MM/DD/YYYY HH:MI:SS AM'), " +
                                    DermagaHeader.lama_rencana_tambat + ", '" +
                                    DermagaHeader.kode_pelabuhan + "', '" +
                                    DermagaHeader.nama_pelabuhan + "', " +
                                    DermagaHeader.kade_awal + ", " + DermagaHeader.kade_akhir + ", '" +
                                    DermagaHeader.kode_pelanggan +
                                  "')";

                    var execute_header = connection.Execute(header, new { booking_id = DermagaHeader.booking_id }, transaction );

                    int[] detail_status = new int[4];
                    int i = 0;
                    foreach (var Item in DermagaHeader.dermaga_detail)
                    {
                        string detail = @"INSERT INTO MAGIC_BOOKING_DETAIL VALUES('" +
                                    Item.booking_id + "', '" +
                                    Item.jasa + "', '" +
                                    Item.asal + "', '" +
                                    Item.tujuan + "', TO_DATE('" +
                                    Item.tgl_mulai + "', 'MM/DD/YYYY HH:MI:SS AM'), TO_DATE('" +
                                    Item.tgl_selesai + "', 'MM/DD/YYYY HH:MI:SS AM'), '" +
                                    Item.jam_mulai + "', '" +
                                    Item.jam_selesai + "', " +
                                    Item.jumlah_gerakan + ", '" +
                                    Item.gerakan + "', " +
                                    Item.epb + ", TO_DATE('" +
                                    Item.created_date + "', 'MM/DD/YYYY HH:MI:SS AM'), '" +
                                    Item.kode_pelabuhan_asal + "', '" +
                                    Item.kode_pelabuhan_tujuan + "'" +
                                  ")";

                        var execute_detail = connection.Execute(detail, new { booking_id = Item.booking_id }, transaction);

                        if (execute_detail == 1)
                        {
                            detail_status[i] = 1;
                        }
                        else
                        {
                            detail_status[i] = 0;
                        }

                        i++;
                    }

                    if (execute_header == 1 && detail_status.Contains(0) == false)
                    {
                        transaction.Commit();

                        result.Add("code", "200");
                        result.Add("status", "success");
                        result.Add("message", "Booking berhasil dikirim");
                    }
                    else
                    {
                        transaction.Rollback();

                        result.Add("code", "500");
                        result.Add("status", "error");
                        result.Add("message", "Terjadi kesalahan.");
                    }
                }
                catch (Exception)
                {
                    result.Add("code", "500");
                    result.Add("status", "error");
                    result.Add("message", "Terjadi kesalahan.");
                }
            }

            return result;
        }

        public IEnumerable<GetBookingDermagaList> getBookingDermaga(BookingDermagaParam bookingParam)
        {
            IEnumerable<GetBookingDermagaList> result = null;

            using (IDbConnection connection = Extension.GetConnection(1))
            {
                try
                {
                    string paramKodePelanggan = "";
                    if (!string.IsNullOrEmpty(bookingParam.kode_pelanggan) && bookingParam.kode_pelanggan!= "string")
                    {
                        paramKodePelanggan = " AND MAGIC_BOOKING_HEADER.KODE_PELANGGAN='" + bookingParam.kode_pelanggan + "'";
                    }

                    var sql = "";
                    if (!string.IsNullOrEmpty(bookingParam.kode_booking) && bookingParam.kode_booking != "string")
                    {
                        sql = "SELECT * FROM MAGIC_BOOKING_HEADER JOIN MAGIC_BOOKING_DETAIL ON MAGIC_BOOKING_HEADER.BOOKING_ID=MAGIC_BOOKING_DETAIL.BOOKING_ID " +
                            "AND MAGIC_BOOKING_HEADER.BOOKING_ID='" + bookingParam.kode_booking + "'" + paramKodePelanggan + " ORDER BY MAGIC_BOOKING_HEADER.BOOKING_ID ASC";
                    }
                    else if (!string.IsNullOrEmpty(bookingParam.kode_pelanggan) && bookingParam.kode_pelanggan != "string")
                    {
                        sql = "SELECT DISTINCT(MAGIC_BOOKING_HEADER.BOOKING_ID), " +
                            "MAGIC_BOOKING_HEADER.CREATED_DATE, " +
                            "MAGIC_BOOKING_HEADER.KOMODITI, " +
                            "MAGIC_BOOKING_HEADER.NAMA_KAPAL, " +
                            "MAGIC_BOOKING_HEADER.KODE_KAPAL, " +
                            "MAGIC_BOOKING_HEADER.GT, " +
                            "MAGIC_BOOKING_HEADER.LOA, " +
                            "MAGIC_BOOKING_HEADER.DRAFT, " +
                            "MAGIC_BOOKING_HEADER.KD_CABANG, " +
                            "MAGIC_BOOKING_HEADER.LAMA_RENCANA_TAMBAT, " +
                            "MAGIC_BOOKING_HEADER.KODE_PELABUHAN, " +
                            "MAGIC_BOOKING_HEADER.NAMA_PELABUHAN, " +
                            "MAGIC_BOOKING_HEADER.KADE_AWAL, " +
                            "MAGIC_BOOKING_HEADER.KADE_AKHIR, " +
                            "MAGIC_BOOKING_HEADER.KODE_PELANGGAN, " +
                            "MAGIC_BOOKING_DETAIL.ASAL, " +
                            "MAGIC_BOOKING_DETAIL.TUJUAN, " +
                            "MAGIC_BOOKING_DETAIL.JAM_MULAI, " +
                            "MAGIC_BOOKING_DETAIL.JAM_SELESAI, " +
                            "MAGIC_BOOKING_DETAIL.JUMLAH_GERAKAN, " +
                            "MAGIC_BOOKING_DETAIL.GERAKAN, " +
                            "MAGIC_BOOKING_DETAIL.KODE_PELABUHAN_ASAL, " +
                            "MAGIC_BOOKING_DETAIL.KODE_PELABUHAN_TUJUAN, " +
                            "MAGIC_BOOKING_DETAIL.TGL_MULAI, " +
                            "MAGIC_BOOKING_DETAIL.TGL_SELESAI " +
                            "FROM MAGIC_BOOKING_HEADER " +
                            "JOIN MAGIC_BOOKING_DETAIL ON MAGIC_BOOKING_HEADER.BOOKING_ID = MAGIC_BOOKING_DETAIL.BOOKING_ID" + paramKodePelanggan + " ORDER BY MAGIC_BOOKING_HEADER.BOOKING_ID ASC";
                    }
                    else
                    {
                        sql = "SELECT DISTINCT(MAGIC_BOOKING_HEADER.BOOKING_ID), " +
                            "MAGIC_BOOKING_HEADER.CREATED_DATE, " +
                            "MAGIC_BOOKING_HEADER.KOMODITI, " +
                            "MAGIC_BOOKING_HEADER.NAMA_KAPAL, " +
                            "MAGIC_BOOKING_HEADER.KODE_KAPAL, " +
                            "MAGIC_BOOKING_HEADER.GT, " +
                            "MAGIC_BOOKING_HEADER.LOA, " +
                            "MAGIC_BOOKING_HEADER.DRAFT, " +
                            "MAGIC_BOOKING_HEADER.KD_CABANG, " +
                            "MAGIC_BOOKING_HEADER.LAMA_RENCANA_TAMBAT, " +
                            "MAGIC_BOOKING_HEADER.KODE_PELABUHAN, " +
                            "MAGIC_BOOKING_HEADER.NAMA_PELABUHAN, " +
                            "MAGIC_BOOKING_HEADER.KADE_AWAL, " +
                            "MAGIC_BOOKING_HEADER.KADE_AKHIR, " +
                            "MAGIC_BOOKING_HEADER.KODE_PELANGGAN, " +
                            "MAGIC_BOOKING_DETAIL.ASAL, " +
                            "MAGIC_BOOKING_DETAIL.TUJUAN, " +
                            "MAGIC_BOOKING_DETAIL.JAM_MULAI, " +
                            "MAGIC_BOOKING_DETAIL.JAM_SELESAI, " +
                            "MAGIC_BOOKING_DETAIL.JUMLAH_GERAKAN, " +
                            "MAGIC_BOOKING_DETAIL.GERAKAN, " +
                            "MAGIC_BOOKING_DETAIL.KODE_PELABUHAN_ASAL, " +
                            "MAGIC_BOOKING_DETAIL.KODE_PELABUHAN_TUJUAN, " +
                            "MAGIC_BOOKING_DETAIL.TGL_MULAI, " +
                            "MAGIC_BOOKING_DETAIL.TGL_SELESAI " +
                            "FROM MAGIC_BOOKING_HEADER " +
                            "JOIN MAGIC_BOOKING_DETAIL ON MAGIC_BOOKING_HEADER.BOOKING_ID = MAGIC_BOOKING_DETAIL.BOOKING_ID ORDER BY MAGIC_BOOKING_HEADER.BOOKING_ID ASC";
                    }

                    result = connection.Query<GetBookingDermagaList>(sql);
                }
                catch (Exception)
                {
                    result = null;
                }
            }

            return result;
        }
    }
}
