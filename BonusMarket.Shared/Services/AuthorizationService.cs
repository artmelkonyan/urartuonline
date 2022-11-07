using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BonusMarket.Shared.Models.Core.Auth;
using BonusMarket.Shared.Models.Core.Permission;
using BonusMarket.Shared.Models.Core.User;
using BonusMarket.Shared.Repository.User;

namespace BonusMarket.Shared.Services
{
    /// <summary>
    /// Authorize Class for checking for permissions
    /// </summary>
    public class AuthorizationService
    {
        private readonly UserRepository _userRepository;
        private readonly EncryptionService _encryptionService;

        public AuthorizationService(UserRepository userRepository, EncryptionService encryptionService)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
        }
        
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }        
        
        /// <summary>
        /// Has Permission method
        /// </summary>
        /// <param name="userPermissions"></param>
        /// <param name="module"></param>
        /// <param name="permissionNumberList"></param>
        /// <returns></returns>
        public static AuthorizationState HasPermission(List<UserRole> userRoles, int module, ICollection<int> permissionNumberList)
        {
            return CheckPermissionsMatch(userRoles, module, permissionNumberList);
        }

        public User Login(string login, string password)
        {
            var user = _userRepository.getByEmail(login);
            
            if (user == null)
                return null;

            // string hashedPassword = _encryptionService.CryptPasswordWithSalt(password, user.PasswordHash);

            string hashedPassword = CalculateMD5Hash(password);

            if (hashedPassword == password && !String.IsNullOrEmpty(password))
                return user;
            
            return _userRepository.getByEmailAndhash(login, hashedPassword);
        }
        
        /// <summary>
        /// Check for permissions
        /// </summary>
        /// <param name="userPermissions"></param>
        /// <param name="module"></param>
        /// <param name="permissionNumberList"></param>
        /// <returns></returns>
        private static AuthorizationState CheckPermissionsMatch(List<UserRole> userRoles, int module, ICollection<int> permissionNumberList)
        {
            try
            {
                if (userRoles == null)
                    return AuthorizationState.Forbidden;

                Dictionary<int, HashSet<int>> userPermissions = new Dictionary<int, HashSet<int>>();

                if (userRoles.Any(role => role.Role.RolePermissions.Any(perm => permissionNumberList.Contains(perm.Permission.PermissionNumber))))
                {
                    return AuthorizationState.HasPermission;
                }

                return AuthorizationState.Forbidden;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string CalculateMD5Hash(string input)

        {

            // step 1, calculate MD5 hash from input

            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)

            {

                sb.Append(hash[i].ToString("X2"));

            }

            return sb.ToString();

        }
    }
}
