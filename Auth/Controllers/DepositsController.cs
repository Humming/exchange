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
            var me = _context.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var deposit = new Deposit();
            deposit.Wallet = me.Wallet;
            //consider saving deposit wallit id somehow.
            return View(deposit);
        }


        public async Task<IActionResult> Index()
        {
            //var me = await this._context.Users.FindAsync(User);
            //change to my deposits
            return View(await this._context.Deposits.ToListAsync());
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
                var me = await _context.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
                deposit.Wallet = me.Wallet;
                me.Wallet.Deposit(deposit);

                await this._context.AddAsync(deposit);
                
                await this._context.SaveChangesAsync();
                return RedirectToAction(nameof(Deposit));
            }

            return View(deposit);
        }
    }
}