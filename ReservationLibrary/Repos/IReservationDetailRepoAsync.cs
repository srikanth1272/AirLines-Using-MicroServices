using ReservationLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationLibrary.Repos
{
    public interface IReservationDetailRepoAsync
    {
        public Task AddReservationDetail(ReservationDetail reservationDetail);
        public Task RemoveReservationDetail(string pnr, int pno);
        public Task UpdateReservationDetail(string pnr, int pno, ReservationDetail reservationDetail);

        Task<ReservationDetail> GetReservationDetail(string pnr, int pno);
        Task<List<ReservationDetail>> GetAllReservationDetails();

        Task<List<ReservationDetail>> GetReservationDetailsByPNR(string pnr);
    }
}
