using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4_Problem_2.ViewModel
{
    /// <summary>
    /// An interface that lets objects be closed.
    /// </summary>
    public interface IClosable
    {
        /// <summary>
        /// Closes this object.
        /// </summary>
        void Close();
    }
}

