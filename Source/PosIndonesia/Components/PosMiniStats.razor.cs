using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace PosIndonesia;
public partial class PosMiniStats
{
    [Parameter]
    public int ProvincesCount { get; set; }

    [Parameter]
    public int RegenciesCount { get; set; }

    [Parameter]
    public int DistrictsCount { get; set; }

    [Parameter]
    public int VillagesCount { get; set; }
}
