using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AxLabelHelper
{
    public static class AxLabelMethods
    {
        private static List<AxLabel> GetLabelsFromFile(string filename)
        {
            var labels = new List<AxLabel>();

            string text = File.ReadAllText($@"{ filename }");

            var lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var labelLines = new List<string>();

            for (int i = 0; i < lines.Length; i++)
            {
                AxLabel label = new AxLabel() { Line1 = lines[i] };

                if ((i < lines.Length - 1) && (lines[i + 1].StartsWith(" ;")))
                {
                    label.Comment = lines[i];
                    i++;
                }

                labels.Add(label);               
            }

            return labels;
        }

        public static Result SortAndCreateMissingLabels(string parentFilename, string childFilename, string targetFilename)
        {
            var result = new Result();


            try
            {
                var parentLabels = GetLabelsFromFile(parentFilename);
                var childLabels = GetLabelsFromFile(childFilename);
                var targetLabels = new List<AxLabel>();

                for (int i = 0; i < parentLabels.Count; i++)
                {
                    var childLabel = childLabels.FirstOrDefault(x => x.LabelId == parentLabels[i].LabelId);

                    var label = new AxLabel()
                    {
                        LabelId = parentLabels[i].LabelId,
                        Description = (childLabel == null ? "=//TODO" : childLabel.Description),
                        Comment = (childLabel == null ? " ;//TODO" : childLabel.Comment)
                    };

                    targetLabels.Add(label);

                    string resultText = String.Join(Environment.NewLine, targetLabels.Select(x => x.FullLabel)) + Environment.NewLine;

                    File.WriteAllText($"{ targetFilename }", resultText);
                }
            }
            catch (Exception ex)
            {
                return new Result { Success = false, Error = ex.Message };
            }

            return new Result { Success = true };
        }
    }
}
