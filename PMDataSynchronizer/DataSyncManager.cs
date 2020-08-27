using System;
using System.Linq;
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
                    var userExists = await db.Users.Where(x => x.Username == user.Username).AnyAsync();
                    if (userExists)
                    {
                        switch (syncMethod)
                        {
                            case DbSyncMethod.Insert:
                                await db.Positions.AddAsync(position);
                                await db.SaveChangesAsync();
                                break;
                            case DbSyncMethod.Update:
                                db.Entry(position).State = EntityState.Modified;
                                await db.SaveChangesAsync();
                                break;
                            case DbSyncMethod.Delete:
                                db.Entry(position).State = EntityState.Deleted;
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
                    var userExists = await db.Users.Where(x => x.Username == user.Username).AnyAsync();
                    if (userExists)
                    {
                        switch (syncMethod)
                        {
                            case DbSyncMethod.Insert:
                                await db.Transactions.AddAsync(transaction);
                                await db.SaveChangesAsync();
                                break;
                            case DbSyncMethod.Update:
                                db.Entry(transaction).State = EntityState.Modified;
                                await db.SaveChangesAsync();
                                break;
                            case DbSyncMethod.Delete:
                                db.Entry(transaction).State = EntityState.Deleted;
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
                    var userExists = await db.Users.Where(x => x.Username == user.Username).AnyAsync();
                    if (userExists)
                    {
                        switch (syncMethod)
                        {
                            case DbSyncMethod.Insert:
                                await db.Orders.AddAsync(order);
                                await db.SaveChangesAsync();
                                break;
                            case DbSyncMethod.Update:
                                db.Entry(order).State = EntityState.Modified;
                                await db.SaveChangesAsync();
                                break;
                            case DbSyncMethod.Delete:
                                db.Entry(order).State = EntityState.Deleted;
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
                    var userExists = await db.Users.Where(x => x.Username == user.Username).AnyAsync();
                    if (userExists)
                    {
                        switch (syncMethod)
                        {
                            case DbSyncMethod.Insert:
                                await db.Accounts.AddAsync(account);
                                await db.SaveChangesAsync();
                                break;
                            case DbSyncMethod.Update:
                                db.Entry(account).State = EntityState.Modified;
                                await db.SaveChangesAsync();
                                break;
                            case DbSyncMethod.Delete:
                                db.Entry(account).State = EntityState.Deleted;
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
                    var userExists = await db.Users.Where(x => x.Username == user.Username).AnyAsync();
                    if (userExists)
                    {
                        switch (syncMethod)
                        {
                            case DbSyncMethod.Insert:
                                await db.Users.AddAsync(user);
                                await db.SaveChangesAsync();
                                break;
                            case DbSyncMethod.Update:
                                db.Entry(user).State = EntityState.Modified;
                                await db.SaveChangesAsync();
                                break;
                            case DbSyncMethod.Delete:
                                db.Entry(user).State = EntityState.Deleted;
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

        public async Task<bool> SyncTokens(Tokens token, Users user, DbSyncMethod syncMethod)
        {
            try
            {
                bool wroteRecord = false;
                await using (var db = new PseudoMarketsDbContext(DbConnectionString))
                {
                    var userExists = await db.Users.Where(x => x.Username == user.Username).AnyAsync();
                    if (userExists)
                    {
                        switch (syncMethod)
                        {
                            case DbSyncMethod.Insert:
                                await db.Tokens.AddAsync(token);
                                await db.SaveChangesAsync();
                                break;
                            case DbSyncMethod.Update:
                                db.Entry(token).State = EntityState.Modified;
                                await db.SaveChangesAsync();
                                break;
                            case DbSyncMethod.Delete:
                                db.Entry(token).State = EntityState.Deleted;
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
