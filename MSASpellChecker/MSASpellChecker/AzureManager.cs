using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSASpellChecker
{
    public class AzureManager
    {

        private static AzureManager instance;
        private MobileServiceClient client;
        private IMobileServiceTable<EasyTableModel> easyTable;

        private AzureManager()
        {
            this.client = new MobileServiceClient("http://mySpellChecker.azurewebsites.net");
            this.easyTable = this.client.GetTable<EasyTableModel>();
        }

        public MobileServiceClient AzureClient
        {
            get { return client; }
        }

        public static AzureManager AzureManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AzureManager();
                }

                return instance;
            }
        }

        public async Task AddToHistory(EasyTableModel easyTableModel)
        {
            await this.easyTable.InsertAsync(easyTableModel);
        }

        public async Task DeleteHistory(EasyTableModel easyTableModel)
        {
            await this.easyTable.DeleteAsync(easyTableModel);
        }

        public async Task<List<EasyTableModel>> GetHistory()
        {
            return await this.easyTable.ToListAsync();
        }
    }
}
