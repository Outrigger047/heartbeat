using HtmlAgilityPack;

namespace Heartbeat
{
    class PageUpdater
    {
        private HtmlDocument _htmlDoc;
        private string _indexFilePath;

        // Const strings - Page ID selectors
        private const string pageInfoTableId = "info-table";
        private const string pageInfoTableHeaderRowId = "table-header-row";

        // Const strings - Table event messages
        private const string heartbeatMsg = "Heartbeat";
        private const string fileLoadMsg = "File loaded";
        private const string newFileWriteMsg = "New file written";

        public PageUpdater(string indexFilePathIn)
        {
            _indexFilePath = indexFilePathIn;
        }

        public enum WriteEventTypes
        {
            FileLoaded, Heartbeat, NewFileWritten
        }

        public void ReadPage()
        {
            _htmlDoc = new HtmlDocument();
            _htmlDoc.Load(_indexFilePath);
        }

        private void TableInsertCurrentState(WriteEventTypes eventType)
        {
            var now = DateTime.Now;
            
            string msg;
            switch (eventType)
            {
                case WriteEventTypes.FileLoaded:
                    msg = fileLoadMsg;
                    break;
                case WriteEventTypes.Heartbeat:
                    msg = heartbeatMsg;
                    break;
                case WriteEventTypes.NewFileWritten:
                    msg = newFileWriteMsg;
                    break;
                default:
                    msg = string.Empty;
                    break;
            }

            var nodeToInsert = _htmlDoc.CreateTextNode(
                @$"<tr>
                     <td>{now.ToShortDateString}</td>
                     <td>{now.ToShortTimeString}</td>
                     <td>{msg}</td>
                     <td></td>
                   </tr>"
                );
        }
    }
}
