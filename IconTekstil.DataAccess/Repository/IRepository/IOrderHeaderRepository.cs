using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IconTekstil.Models;

namespace IconTekstil.DataAccess.Repository.IRepository
{
	public interface IOrderHeaderRepository : IRepository<OrderHeader>
	{
		void Update(OrderHeader obj);
		void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);

		void UpdateStripePaymentPaymentId(int id, string s, string sessionId, string paymentId);
       
    }
}
