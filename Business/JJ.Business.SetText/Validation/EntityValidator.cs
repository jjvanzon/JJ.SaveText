using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Validation;
using JJ.Models.SetText;
using JJ.Business.SetText.Resources;

namespace JJ.Business.SetText.Validation
{
    public class EntityValidator : FluentValidator<Entity>
    {
        public EntityValidator(Entity entity)
            : base(entity)
        { }

        protected override void Execute()
        {
            For(() => Object.Text, PropertyDisplayNames.Text)
                .NotNullOrWhiteSpace();
        }
    }
}
