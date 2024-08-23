using ReservationLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationLibrary.Repos
{
    public class EFReservationDetailRepoAsync:IReservationDetailRepoAsync
    {
        ReservationDBContext ctx = new ReservationDBContext();
        public async Task AddReservationDetail(ReservationDetail reservationDetail)
        {
            try
            {
                await ctx.AddAsync(reservationDetail);
                await ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ReservationException(ex.Message);
            }
        }

        public async Task<List<ReservationDetail>> GetAllReservationDetails()
        {
            List<ReservationDetail> reservations = await ctx.ReservationDetails.ToListAsync();
            return reservations;
        }

        public async Task<ReservationDetail> GetReservationDetail(string pnr, int pno)
        {
            try
            {
                ReservationDetail reservationDetail = await (from r in ctx.ReservationDetails where r.PNR == pnr && r.PassengerNo == pno select r).FirstAsync();
                return reservationDetail;
            }
            catch
            {
                throw new ReservationException("No Reservation Details found");
            }
        }

        public async Task<List<ReservationDetail>> GetReservationDetailsByPNR(string pnr)
        {
            List<ReservationDetail> reservationDetails = await (from r in ctx.ReservationDetails where r.PNR == pnr select r).ToListAsync();
            return reservationDetails;
        }

        public async Task RemoveReservationDetail(string pnr, int pno)
        {
            try
            {
                ReservationDetail reservationDetail = await GetReservationDetail(pnr, pno);
                ctx.ReservationDetails.Remove(reservationDetail);
                await ctx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new ReservationException(e.Message);
            }
        }

        public async Task UpdateReservationDetail(string pnr, int pno, ReservationDetail reservationDetail)
        {
            try
            {
                ReservationDetail reservationDetail2 = await GetReservationDetail(pnr, pno);
                reservationDetail2.PassengerName = reservationDetail.PassengerName;
                reservationDetail2.Age = reservationDetail.Age;
                reservationDetail2.Gender = reservationDetail.Gender;
                await ctx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new ReservationException(e.Message);
            }
        }
    }
}
