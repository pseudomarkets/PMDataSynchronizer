using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PMUnifiedAPI.Models;

/*
 * Pseudo Markets Data Sync Manager
 * Author: Shravan Jambukesan <shravan@shravanj.com>
 * (c) 2019 - 2020 Pseudo Markets
 */

namespace PMDataSynchronizer
{
    public class DataSyncManager
    {
        private static string DbConnectionString = "";

        public enum DbSyncMethod : int
        {
            Insert = 1,
            Update = 2,
            Delete = 3
        }

        public DataSyncManager(string connectionString)
        {
            DbConnectionString = connectionString;
        }

        public async Task<bool> SyncPositions(Positions position, Users user, DbSyncMethod syncMethod)
        {
            try
            {
                bool wroteRecord = false;
                await using (var db = new PseudoMarketsDbContext(DbConnectionString))
                {
                    var userExists = db.Users.Any(x => x.Username == user.Username);
                    if (userExists)
                    {
                        var localUser = db.Users.FirstOrDefault(x => x.Username == user.Username);
                        var accountId = db.Accounts.FirstOrDefault(x => x.UserID == localUser.Id).Id;
                        var existingPosition = await db.Positions
                            .Where(x => x.AccountId == accountId && x.Symbol == position.Symbol)
                            .FirstOrDefaultAsync();
                        switch (syncMethod)
                        {
                            case DbSyncMethod.Insert:
                                Positions localPosition = new Positions()
                                {
                                    OrderId = position.OrderId,
                                    Symbol = position.Symbol,
                                    AccountId = accountId,
                                    Quantity = position.Quantity,
                                    Value = position.Value
                                };
                                await db.Positions.AddAsync(localPosition);
                                await db.SaveChangesAsync();
                                break;
                            case DbSyncMethod.Update:
                                existingPosition.Value = position.Value;
                                existingPosition.Quantity = position.Quantity;
                                db.Entry(existingPosition).State = EntityState.Modified;
                                await db.SaveChangesAsync();
                                break;
                            case DbSyncMethod.Delete:
                                db.Entry(existingPosition).State = EntityState.Deleted;
                                await db.SaveChangesAsync();
                                break;
                        }
                        wroteRecord = true;
                    }
                    else
                    {
                        wroteRecord = false;
                    }
                }

                return wroteRecord;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> SyncTransactions(Transactions transaction, Users user, DbSyncMethod syncMethod)
        {
            try
            {
                bool wroteRecord = false;
                await using (var db = new PseudoMarketsDbContext(DbConnectionString))
                {
                    var userExists = db.Users.Any(x => x.Username == user.Username);
                    if (userExists)
                    {
                        switch (syncMethod)
                        {
                            case DbSyncMethod.Insert:
                                var localUser = db.Users.FirstOrDefault(x => x.Username == user.Username);
                                var accountId = db.Accounts.FirstOrDefault(x => x.UserID == localUser.Id).Id;
                                Transactions localTransaction = new Transactions()
                                {
                                    TransactionId = transaction.TransactionId,
                                    AccountId = accountId
                                };
                                await db.Transactions.AddAsync(localTransaction);
                                await db.SaveChangesAsync();
                                break;
                        }
                        wroteRecord = true;
                    }
                    else
                    {
                        wroteRecord = false;
                    }
                }

                return wroteRecord;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> SyncOrders(Orders order, Users user, DbSyncMethod syncMethod)
        {
            try
            {
                bool wroteRecord = false;
                await using (var db = new PseudoMarketsDbContext(DbConnectionString))
                {
                    var userExists = db.Users.Any(x => x.Username == user.Username);
                    if (userExists)
                    {
                        switch (syncMethod)
                        {
                            case DbSyncMethod.Insert:
                                Orders localOrder = new Orders()
                                {
                                    Price = order.Price,
                                    Quantity = order.Quantity,
                                    Date = order.Date,
                                    Symbol = order.Symbol,
                                    TransactionID = order.TransactionID,
                                    Type = order.Type
                                };
                                await db.Orders.AddAsync(localOrder);
                                await db.SaveChangesAsync();
                                break;
                        }
                        wroteRecord = true;
                    }
                    else
                    {
                        wroteRecord = false;
                    }
                }

                return wroteRecord;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> SyncAccounts(Accounts account, Users user, DbSyncMethod syncMethod)
        {
            try
            {
                bool wroteRecord = false;
                await using (var db = new PseudoMarketsDbContext(DbConnectionString))
                {
                    var userExists = db.Users.Any(x => x.Username == user.Username);
                    if (userExists)
                    {
                        var localUser = db.Users.FirstOrDefault(x => x.Username == user.Username);
                        var existingAccount = db.Accounts.FirstOrDefault(x => x.UserID == localUser.Id);
                        switch (syncMethod)
                        {
                            case DbSyncMethod.Update:
                                existingAccount.Balance = account.Balance;
                                db.Entry(existingAccount).State = EntityState.Modified;
                                await db.SaveChangesAsync();
                                break;
                            case DbSyncMethod.Delete:
                                db.Entry(existingAccount).State = EntityState.Deleted;
                                await db.SaveChangesAsync();
                                break;
                        }
                        wroteRecord = true;
                    }
                    else
                    {
                        wroteRecord = false;
                    }
                }

                return wroteRecord;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> SyncUsers(Users user, DbSyncMethod syncMethod)
        {
            try
            {
                bool wroteRecord = false;
                await using (var db = new PseudoMarketsDbContext(DbConnectionString))
                {
                    var userExists = db.Users.Any(x => x.Username == user.Username);
                    if (userExists)
                    {
                        var existingUser = db.Users.FirstOrDefault(x => x.Username == user.Username);
                        switch (syncMethod)
                        {
                            case DbSyncMethod.Update:
                                existingUser.Password = user.Password;
                                existingUser.Salt = user.Salt;
                                db.Entry(existingUser).State = EntityState.Modified;
                                await db.SaveChangesAsync();
                                break;
                            case DbSyncMethod.Delete:
                                db.Entry(existingUser).State = EntityState.Deleted;
                                await db.SaveChangesAsync();
                                break;
                        }
                        wroteRecord = true;
                    }
                    else
                    {
                        wroteRecord = true;
                    }
                }

                return wroteRecord;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> SyncNewUser(Users user, string token)
        {
            try
            {
                bool wroteRecord = false;
                await using (var db = new PseudoMarketsDbContext(DbConnectionString))
                {
                    var userExists = db.Users.Any(x => x.Username == user.Username);
                    if (!userExists)
                    {
                        await db.Users.AddAsync(user);
                        await db.SaveChangesAsync();
                        Users createdUser = db.Users.FirstOrDefault(x => x.Username == user.Username);
                        Tokens newToken = new Tokens()
                        {
                            UserID = createdUser.Id,
                            Token = token
                        };
                        Accounts newAccount = new Accounts()
                        {
                            UserID = createdUser.Id,
                            Balance = 1000000.99
                        };
                        await db.Tokens.AddAsync(newToken);
                        await db.Accounts.AddAsync(newAccount);
                        await db.SaveChangesAsync();
                        wroteRecord = true;
                    }
                    else
                    {
                        wroteRecord = false;
                    }
                }

                return wroteRecord;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> SyncTokens(Tokens token, Users user, DbSyncMethod syncMethod)
        {
            try
            {
                bool wroteRecord = false;
                await using (var db = new PseudoMarketsDbContext(DbConnectionString))
                {
                    var userExists = db.Users.Any(x => x.Username == user.Username);
                    if (userExists)
                    {
                        var existingToken = db.Tokens.FirstOrDefault(x => x.UserID == user.Id);
                        switch (syncMethod)
                        {
                            case DbSyncMethod.Update:
                                existingToken.Token = token.Token;
                                db.Entry(existingToken).State = EntityState.Modified;
                                await db.SaveChangesAsync();
                                break;
                            case DbSyncMethod.Delete:
                                db.Entry(existingToken).State = EntityState.Deleted;
                                await db.SaveChangesAsync();
                                break;
                        }
                        wroteRecord = true;
                    }
                    else
                    {
                        wroteRecord = false;
                    }
                }

                return wroteRecord;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }


}
