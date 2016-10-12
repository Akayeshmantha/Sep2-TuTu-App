using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Tu_Tu.Model.Entities;
using Tu_Tu.Model.Persistence;
using Tu_Tu.Models;
using Tu_Tu.Controllers;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace Tu_Tu
{
    /// <summary>
    /// Class AuthRepository.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class AuthRepository : IDisposable
    {
        /// <summary>
        /// The shopping context
        /// </summary>
        private readonly Tu_Tu_Request_Context tutu_Context;

        /// <summary>
        /// The _user manager
        /// </summary>
        private UserManager<ApplicationUser> userManager;

        public static string UserId = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthRepository"/> class.
        /// </summary>
        public AuthRepository()
        {
            tutu_Context = new Tu_Tu_Request_Context();
            var provider = new DpapiDataProtectionProvider("Sample");
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(tutu_Context));
            //accountController = new AccountController();
            userManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(
            provider.Create("EmailConfirmation"));
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns>Task&lt;IdentityResult&gt;.</returns>
        public static string code = null;
        public async Task<IdentityResult> RegisterUser(ApplicationUser user, string password)
        {

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                code = await userManager.GenerateEmailConfirmationTokenAsync(user.Id);

                //    accountController.confirmEmailLinks(user.Id,code);

                //    Uri callBackUrl = new Uri(AccountController.callBackUrl);
                //    await userManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callBackUrl + "\">here</a>");

                //    Uri locationHeader = new Uri(AccountController.headerLocation);

             }
                return result;
        }
        //public async Task<string> GenerateTokenForConfirmation(string userId)
        //{
        //    string code = await userManager.GenerateEmailConfirmationTokenAsync(userId);

        //    return code;
        //}
        
        public async void SendMail(string userId,Uri callbackURL)
        {
            //Uri callBackUrl = new Uri(AccountController.callBackUrl);

        userManager.EmailService = new Tu_Tu.EmailService();
        await userManager.SendEmailAsync(userId, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackURL + "\">here</a>");
        }
        public async Task<IdentityResult> ConfirmEmail(string userId , string code)
        {
            IdentityResult result = await userManager.ConfirmEmailAsync(userId, code);

            return result;
        }
        /// <summary>
        /// Finds the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns>Task&lt;IdentityUser&gt;.</returns>
        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await userManager.FindAsync(userName, password);
            return user;
        }

        /// <summary>
        /// Returns the awailability of name
        /// </summary>
        /// <param name="userName">Given Name</param>
        /// <returns></returns>
        public async Task<bool> FindByNameAsync(string userName)
        {
            IdentityUser user = await userManager.FindByNameAsync(userName);

            if (user != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the availability of the email
        /// </summary>
        /// <param name="email">Given Email address</param>
        /// <returns></returns>
        public async Task<bool> FindUserByEmail(string email)
        {
            IdentityUser user = await userManager.FindByEmailAsync(email);

            if (user != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Finds the client.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns>Client.</returns>
        public User_TuTU FindClient(string clientId)
        {
            var client = tutu_Context.UserTu_Tu.Find(clientId);

            return client;
        }

        /// <summary>
        /// Adds the refresh token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            var existingToken = tutu_Context.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            tutu_Context.RefreshTokens.Add(token);

            return await tutu_Context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Removes the refresh token.
        /// </summary>
        /// <param name="refreshTokenId">The refresh token identifier.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await tutu_Context.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                tutu_Context.RefreshTokens.Remove(refreshToken);
                return await tutu_Context.SaveChangesAsync() > 0;
            }

            return false;
        }

        /// <summary>
        /// Removes the refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            tutu_Context.RefreshTokens.Remove(refreshToken);
            return await tutu_Context.SaveChangesAsync() > 0;
        }
        
        /// <summary>
        /// Finds the refresh token.
        /// </summary>
        /// <param name="refreshTokenId">The refresh token identifier.</param>
        /// <returns>Task&lt;RefreshToken&gt;.</returns>
        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await tutu_Context.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        /// <summary>
        /// Gets all refresh tokens.
        /// </summary>
        /// <returns>List&lt;RefreshToken&gt;.</returns>
        public List<RefreshToken> GetAllRefreshTokens()
        {
            return tutu_Context.RefreshTokens.ToList();
        }

        /// <summary>
        /// find as an asynchronous operation.
        /// </summary>
        /// <param name="loginInfo">The login information.</param>
        /// <returns>Task&lt;IdentityUser&gt;.</returns>
        public async Task<ApplicationUser> FindAsync(UserLoginInfo loginInfo)
        {
            ApplicationUser user = await userManager.FindAsync(loginInfo);

            return user;
        }

        /// <summary>
        /// create as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Task&lt;IdentityResult&gt;.</returns>
        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            var result = await userManager.CreateAsync(user);

            return result;
        }

        /// <summary>
        /// add login as an asynchronous operation.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="login">The login.</param>
        /// <returns>Task&lt;IdentityResult&gt;.</returns>
        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await userManager.AddLoginAsync(userId, login);

            return result;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            tutu_Context.Dispose();
            userManager.Dispose();

        }
    }
}