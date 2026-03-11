using HIS.Domain.Entities;
using HIS.Domain.Interfaces;
using HIS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Infrastructure.Repositories
{
    public  class DiagonsisRepository:BaseRepository<Diagnosis>,IDiagonsisRepository
    {
        private readonly HISDbContext context;

        public DiagonsisRepository(HISDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Diagnosis?>> GetDiagnosesByEncounterIdAsync(Guid EncounterId,CancellationToken cancellation=default)   
        {
         return   await context.Diagnoses.Where(x => x.EncounterId ==EncounterId).ToListAsync(cancellation); 
        }

        
    }
    
}
