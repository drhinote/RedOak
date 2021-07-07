using EFHooks;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roi.Data
{
    public class SubjectMaskHook : PreInsertHook<Subject>
    {
        public override EntityState HookStates => EntityState.Added | EntityState.Modified | EntityState.Unchanged | EntityState.Detached;

        public override void Hook(Subject entity, HookEntityMetadata metadata)
        {
            if(!entity.Name.IsBase64String()) entity.Name = entity.Name.ToBase64();
            if(!entity.Social.IsBase64String()) entity.Social = entity.Social.ToBase64();
        }
    }

    public class SubjectMaskHook2 : PreUpdateHook<Subject>
    {
        public override EntityState HookStates => EntityState.Added | EntityState.Modified | EntityState.Unchanged | EntityState.Detached;

        public override void Hook(Subject entity, HookEntityMetadata metadata)
        {
            if (!entity.Name.IsBase64String()) entity.Name = entity.Name.ToBase64();
            if (!entity.Social.IsBase64String()) entity.Social = entity.Social.ToBase64();
        }
    }

    public class SubjectUnmaskHook : PostLoadHook<Subject>
    {
        public override EntityState HookStates => EntityState.Added | EntityState.Modified | EntityState.Unchanged | EntityState.Detached;

        public override void Hook(Subject entity, HookEntityMetadata metadata)
        {
            if(entity.Name.IsBase64String()) entity.Name = entity.Name.FromBase64();
            if(entity.Social.IsBase64String()) entity.Social = entity.Social.FromBase64();
        }
    }

    public class SubjectUnmaskHook2 : PostUpdateHook<Subject>
    {
        public override EntityState HookStates => EntityState.Added | EntityState.Modified | EntityState.Unchanged | EntityState.Detached;

        public override void Hook(Subject entity, HookEntityMetadata metadata)
        {
            if (entity.Name.IsBase64String()) entity.Name = entity.Name.FromBase64();
            if (entity.Social.IsBase64String()) entity.Social = entity.Social.FromBase64();
        }
    }

    public class SubjectUnmaskHook3 : PostInsertHook<Subject>
    {
        public override EntityState HookStates => EntityState.Added | EntityState.Modified | EntityState.Unchanged | EntityState.Detached;

        public override void Hook(Subject entity, HookEntityMetadata metadata)
        {
            if (entity.Name.IsBase64String()) entity.Name = entity.Name.FromBase64();
            if (entity.Social.IsBase64String()) entity.Social = entity.Social.FromBase64();
        }
    }

    public class TesterHashHook : PreActionHook<Tester>
    {
        public override EntityState HookStates => EntityState.Added | EntityState.Modified;

        public override void Hook(Tester entity, HookEntityMetadata metadata)
        {
            if(!entity.Password.IsBase64String()) entity.Password = entity.GetPasswordHash();
        }
    }

    public class TesterHashLoadHook : PostActionHook<Tester>
    {
        public override EntityState HookStates => EntityState.Added | EntityState.Modified | EntityState.Detached | EntityState.Unchanged;

        public override void Hook(Tester entity, HookEntityMetadata metadata)
        {
            if (!entity.Password.IsBase64String()) entity.Password = entity.GetPasswordHash();
        }
    }
}
