using FMG.NRT.Components.Extensions;
using FMG.NRT.Objects;
using Xunit;

namespace FMG.NRT.Tests.Unit.Objects
{
    public class RoleViewTests
    {
        #region RoleView()

        [Fact]
        public void RoleView_CreatesEmpty()
        {
            MvcTree actual = new RoleView().Permissions;

            Assert.Empty(actual.SelectedIds);
            Assert.Empty(actual.Nodes);
        }

        #endregion
    }
}
