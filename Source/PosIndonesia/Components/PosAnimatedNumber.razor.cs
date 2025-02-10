using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PosIndonesia.Utilities;

namespace PosIndonesia;
public partial class PosAnimatedNumber : PosJSComponentBase
{
    public PosAnimatedNumber() : base("PosAnimatedNumber")
    {
        Id = IDGenerator.NewId();
    }

    protected override async Task OnParametersSetAsync()
    {
        if (Number > 0)
        {
            await InvokeVoidAsync("animate", Id, Number);
        }
    }

}
