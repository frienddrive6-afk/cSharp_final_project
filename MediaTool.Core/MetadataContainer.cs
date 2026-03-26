namespace MediaTool.Core;

using System.Collections;
using System.Collections.Generic;

public class MetadataContainer //: IEnumerable<object>
{
    public CommonMetadata? Common { get; set; }

    public PhotoMetadata? Photo { get; set; }
    public VideoMetadata? Video { get; set; }



    // public IEnumerator<object> GetEnumerator()
    // {
    //     if (Common != null) yield return Common;

    //     if (Photo != null) yield return Photo;

    //     if (Video != null) yield return Video;
    // }

    // IEnumerator IEnumerable.GetEnumerator()
    // {
    //     return GetEnumerator();
    // }
}