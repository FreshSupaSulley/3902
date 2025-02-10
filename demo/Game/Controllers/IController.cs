using System;
namespace Game.Controllers
{
    public interface IController
    {
        void Update(Object cntrlrStt);
        void PostUpdate();
    }
}
