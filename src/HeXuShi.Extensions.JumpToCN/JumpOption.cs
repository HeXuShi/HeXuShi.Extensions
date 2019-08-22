using System;
using System.Collections.Generic;
using System.Text;

namespace HeXuShi.Extensions
{
    public enum JumpOption
    {
        OnlyTo_SpecSuffix,//Only jump to specify suffix
        OnlyTo_SpecDomain,//Only jump to specify domain
        SuffixTo_SpecSuffix,//specify suffix to specify suffix,
        DomainTo_SpecDomain,//specify domain to specify domain,
    }
}
