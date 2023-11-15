using System.Collections;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Desktop.Pages;

public sealed class PageBindings : IEnumerable<Page>
{
    private Dictionary<string, Page> Pages { get; } = new();

    public void Add(Page page)
    {
        Pages.Add(page.GetType().Name, page);
    }

    public IEnumerator<Page> GetEnumerator()
    {
        return Pages.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}