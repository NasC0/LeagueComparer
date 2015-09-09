using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using ComparerAuthenticationProofOfConcept.Infrastructure;
using ComparerAuthenticationProofOfConcept.Models;

namespace ComparerAuthenticationProofOfConcept.Controllers 
{
    [Authorize(Roles="Admin")]
    public class CompanyController : ApiController
    {
        private IDbContext _dbContext;

        public CompanyController(IDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IHttpActionResult> Get()
        {
            return Ok(_dbContext.Companies.AsQueryable());
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        public async Task<IHttpActionResult> Post(Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Company not valid");
            }

            var companyExists = await _dbContext.Companies.AnyAsync(c => c.Id == company.Id);

            if (companyExists)
            {
                return BadRequest("Company exists");
            }

            _dbContext.Companies.Add(company);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        public async Task<IHttpActionResult> Put(Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Company not valid");
            }

            var companyExists = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == company.Id);

            if (companyExists == null)
            {
                return BadRequest("Company exists");
            }

            companyExists.Name = company.Name;
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == id);

            if (company == null)
            {
                return NotFound();
            }

            _dbContext.Companies.Remove(company);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
