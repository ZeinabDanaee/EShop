using IDP.Domain.IRepository.Command.Base;
using IDP.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Infra.Repository.Command.Base
{
    public class CommandRepository<T> : ICommandRepository<T> where T : class
    {
        protected readonly ShopCommandDbContext _context;

        public CommandRepository(ShopCommandDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<T> Insert(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Update(T entity)
        {
           try
            {
                _context.Entry(entity).State=Microsoft.EntityFrameworkCore.EntityState.Modified;    
                await _context.SaveChangesAsync();
                return true;

            }
            catch(Exception ex) 
            {
          Console.WriteLine(ex.Message);
                return false;   
            }
        }
    }
}
