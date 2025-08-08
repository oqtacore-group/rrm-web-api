using System;

namespace Oqtacore.Rrm.Application.Queries.SiteSettings
{
    public class GetSiteSettingQuery : SingleQuery<GetSiteSettingQueryResult>
    {
        public string Name { get; set; }
    }
    public class GetSiteSettingQueryResult : SingleQueryResult<GetSiteSettingQueryResultItem>
    {
    }
    public class GetSiteSettingQueryResultItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
