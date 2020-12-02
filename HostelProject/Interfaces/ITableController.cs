using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostelProject.Interfaces
{
    public interface ITableController<T>
               where T : class, IDataBaseViewMode
    {
        public IActionResult Index();

        public Task<IActionResult> Edit(int id);

        [HttpPost]
        public Task<IActionResult> Edit(T viewModel);

        public Task<IActionResult> Delete(int id);

        [HttpPost]
        public Task<IActionResult> Delete(T viewModel);

        public IActionResult Create();

        [HttpPost]
        public Task<IActionResult> Create(T viewModel);
    }
}
