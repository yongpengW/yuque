using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Infrastructure.Dtos
{
    public abstract class DtoBase : DtoBase<long>
    {
        public override long Id { get; set; }
    }

    public abstract class DtoBase<TKey> : Dto<TKey>
    {
    }
}
