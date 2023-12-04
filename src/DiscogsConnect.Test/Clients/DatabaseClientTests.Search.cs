using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace DiscogsConnect.Clients;
public partial class DatabaseClientTests
{
    [Fact]
    public async Task SearchReleaseAsync()
    {
        var criteria = new SearchCriteria
        {
            Type = ResourceType.Release,
            Title = "Serious Beats 55",
            Country = "Belgium"
        };

        var response = await Client.Database.SearchAsync(criteria);

        response.Should().NotBeNull();

        response.Items.Should().NotBeEmpty();
        response.Items.Should().HaveCount(1);

        var release = response.Items.Cast<ReleaseSearchResult>().First();

        release.Country.Should().Be("Belgium");
        release.Year.Should().Be("2007");
        release.Format.Should().Contain("CD", "Compilation");
        release.Label.Should().Contain("541", "Serious Beats", "N.E.W.S", "DOCdata Benelux");
        release.Type.Should().Be(ResourceType.Release);
        release.Genre.Should().ContainSingle("Electronic");
        release.Style.Should().ContainSingle("House");
        release.Id.Should().Be(1017350);
        release.Barcode.Should().Contain("5 414165 018254", "SABAM", "NEWS541416501825", "NEWS541416501825-2", "NEWS541416501825-3", "N01.1825.025");
        release.Uri.Should().Be("/release/1017350-Various-Serious-Beats-55");
        release.Catno.Should().Be("541416 501825");
        release.Title.Should().Be("Various - Serious Beats 55");
        release.ResourceUrl.Should().Be("https://api.discogs.com/releases/1017350");
        release.FormatQuantity.Should().Be(3);

        var format = release.Formats.FirstOrDefault();
        format.Should().NotBeNull();
        format.Name.Should().Be("CD");
        format.Qty.Should().Be("3");
        format.Descriptions.Should().Contain("Compilation");
    }

    [Fact]
    public async Task SearchArtistAsync()
    {
        var criteria = new SearchCriteria
        {
            Type = ResourceType.Artist,
            Title = "Emmanuel Top"
        };

        var response = await Client.Database.SearchAsync(criteria);

        response.Should().NotBeNull();

        response.Items.Should().NotBeEmpty();
        response.Items.Should().HaveCount(1);

        var release = response.Items.Cast<ArtistSearchResult>().First();

        release.Id.Should().Be(1240);
        release.Type.Should().Be(ResourceType.Artist);
        release.Uri.Should().Be("/artist/1240-Emmanuel-Top");
        release.Title.Should().Be("Emmanuel Top");
    }

    [Fact]
    public async Task SearchLabelAsync()
    {
        var criteria = new SearchCriteria
        {
            Type = ResourceType.Label,
            Title = "N.E.W.S."
        };

        var response = await Client.Database.SearchAsync(criteria);

        response.Should().NotBeNull();

        response.Items.Should().NotBeEmpty();
        response.Items.Should().HaveCountGreaterThanOrEqualTo(1);

        var label = response.Items.FirstOrDefault(x => x.Id == 26064);

        label.Should().NotBeNull();
        label.Id.Should().Be(26064);
        label.Type.Should().Be(ResourceType.Label);
        label.Uri.Should().Be("/label/26064-NEWS");
        label.Title.Should().Be("N.E.W.S.");
    }

    [Fact]
    public async Task SearchMasterAsync()
    {
        var criteria = new SearchCriteria
        {
            Type = ResourceType.Master,
            Title = "Serious Beats 25"
        };

        var response = await Client.Database.SearchAsync(criteria);

        response.Should().NotBeNull();

        response.Items.Should().NotBeEmpty();
        response.Items.Should().HaveCount(1);

        var master = response.Items.Cast<MasterSearchResult>().First();

        master.Country.Should().Be("Belgium");
        master.Year.Should().Be("1999");
        master.Format.Should().Contain("Vinyl", "12\"", "Compilation", "33 \u2153 RPM");
        master.Label.Should().Contain("541", "Serious Beats", "Serious Beats 25");
        master.Type.Should().Be(ResourceType.Master);
        master.Genre.Should().Contain("Electronic");
        master.Style.Should().Contain("Acid House", "Techno");
        master.Id.Should().Be(1138295);
        master.Barcode.Should().Contain("5 414165 003038", "541416 500 303 - A1", "541416 500 303 - B1");
        master.Uri.Should().Be("/master/1138295-Various-Serious-Beats-25");
        master.Catno.Should().Be("541416 500303");
        master.Title.Should().Be("Various - Serious Beats 25");
        master.ResourceUrl.Should().Be("https://api.discogs.com/masters/1138295");
    }
}
