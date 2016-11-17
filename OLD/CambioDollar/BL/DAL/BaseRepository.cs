using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class TipoCambioRepository : ITipoCambio
    {

        protected CambioDolarEntities _context;

        public TipoCambioRepository()
        {
            _context = new CambioDolarEntities();
        }

    }
}
