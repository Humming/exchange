using System.Linq;
using System.Threading.Tasks;

using Auth.Data;
using Auth.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auth.Controllers
{
    [Authorize]
    public class DepositsController : Controller
    {
        private readonly ApplicationDbContext _context;


        public DepositsController(ApplicationDbContext context)
        {
            this._context = context;
        }


        public IActionResult Deposit()
        {
            var me = _context.Users.Include(y=> y.Wallets).FirstOrDefault(x => x.UserName == User.Identity.Name);
            var deposit = new Deposit();
            deposit.Wallet = me.Wallets.FirstOrDefault();
            //consider saving deposit wallit id somehow.
            return View(deposit);
        }


        public async Task<IActionResult> Index()
        {
            var me = await this._context.Users.Include(x => x.Wallets).FirstOrDefaultAsync(y => y.UserName == User.Identity.Name);

            var d = this._context.Wallets.Include(x => x.Deposits).SelectMany(y=>y.Deposits);
            //change to my deposits
            /*await this._context.Deposits.Include(x=>x.Wallet).ToListAsync()*/
            /*me.Wallets.Select(x=>x.Deposits);*/
            var deposits = d.AsEnumerable<Deposit>();
            return base.View(deposits);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit(Deposit deposit)
        {
            if (deposit.Wallet == null)
            {
                //tell the user that it has to create a wallet first.
                return View(deposit);
            }
            if (ModelState.IsValid)
            {
                var me = await _context.Users.Include(y=>y.Wallets).FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
                deposit.Wallet = me.Wallets.FirstOrDefault();
                deposit.Wallet.Deposit(deposit.Amount);

                await this._context.AddAsync(deposit);
                
                await this._context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(deposit);
        }
    }
}