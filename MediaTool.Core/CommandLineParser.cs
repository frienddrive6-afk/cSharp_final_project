namespace MediaTool.Core;



public static class CommandLineParser
{

    public static AppOptions Parse(string[] args)
    {
        var options = new AppOptions();

        for(int i = 0; i < args.Length; i++)
        {
            switch(args[i])
            {
                case "--photo":
                case "-ph":
                    {
                        options.OperationType = "photo";
                        options.Path = args[++i];
                        break;
                    }


                case "--folder":
                case "-f":
                    {
                        options.OperationType = "folder";
                        options.Path = args[++i];
                        break;
                    }

                case "--whitelist":
                case "-w":
                    {
                        options.WhiteList.Add(args[++i]);
                        break;
                    }

                case "--blacklist":
                case "-bl":
                {
                    options.BlackList.Add(args[++i]);
                    break;
                }

                case "--min-size":
                {
                    options.MinSize = ParseSize(args[++i]);
                    break;
                }

                case "--max-size":
                {
                    options.MaxSize = ParseSize(args[++i]);
                    break;
                }

                case "--author":
                {
                    options.IsWriteMode = true;
                    options.WriteValues.Common ??= new CommonMetadata();
                    options.WriteValues.Common.Author = args[++i];
                    break;        
                }

                case "--iso":
                {
                    options.IsWriteMode = true;
                    options.WriteValues.Photo ??= new PhotoMetadata();
                    options.WriteValues.Photo.Iso = int.Parse(args[++i]);
                    break;
                }

                case "--date":
                {
                    options.IsWriteMode = true;
                    options.WriteValues.Common ??= new CommonMetadata();
                    DateTime dt = DateTime.Parse(args[++i]);
                    options.WriteValues.Common.CreationDate = dt;
                    break;        
                }

                case "--fnumber":
                {
                    options.IsWriteMode = true;
                    options.WriteValues.Photo ??= new PhotoMetadata();
                    options.WriteValues.Photo.FNumber = double.Parse(args[++i]).ToString();
                    break;
                }

                case "--exposure":
                {
                    options.IsWriteMode = true;
                    options.WriteValues.Photo ??= new PhotoMetadata();
                    options.WriteValues.Photo.ExposureTime = double.Parse(args[++i]).ToString();
                    break;
                }

                // case "--title":
                // {
                //     options.IsWriteMode = true;
                //     options.WriteValues.Common ??= new CommonMetadata();
                //     options.WriteValues.Common.Title = args[++i];
                //     break;
                // }

            }
        }

        return options;

    }



    private static long ParseSize(string input)
    {
        long multiplier = 1;

        if(input.EndsWith("k",StringComparison.OrdinalIgnoreCase))
        {
            multiplier = 1024;        
        }else if(input.EndsWith("m",StringComparison.OrdinalIgnoreCase))
        {
            multiplier = 1024 * 1024;
        }

        string numberPart = input.TrimEnd('k','K','m','M');
        return long.Parse(numberPart) * multiplier;

    }

}