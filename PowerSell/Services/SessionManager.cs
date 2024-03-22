using PowerSell.Models;
using System;
using System.Linq;

namespace PowerSell.Services
{
    public class SessionManager
    {
        private static readonly object _lock = new object();
        private static SessionManager _instance;

        public static SessionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SessionManager();
                        }
                    }
                }
                return _instance;
            }
        }

        private int _userId;
        private string _userName;

        public int UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                // Call a method to fetch UserName based on UserId
                _userName = FetchUserName(_userId);
            }
        }

        public string UserName => _userName ?? "Unknown User";

        private string FetchUserName(int userId)
        {
            using (var dbContext = new PowerSellDbContext())
            {
                var user = dbContext.Users.FirstOrDefault(u => u.UserId == userId);
                return user?.UserName;
            }
        }
    }
}
