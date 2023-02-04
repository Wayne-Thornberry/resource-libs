using Newtonsoft.Json;

using Proline.Resource.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTrading.Scripts
{
    public class InitCore
    {
        private DeleteItemRequestAction _deleteItemAction;
        private ListItemsRequestAction _listItemsAction;
        private PutItemRequestAction _putItemsAction;

        public async Task Execute()
        {
            try
            {
                _deleteItemAction = new DeleteItemRequestAction();
                _listItemsAction = new ListItemsRequestAction();
                _putItemsAction = new PutItemRequestAction();

                _deleteItemAction.Subscribe();
                _listItemsAction.Subscribe();
                _putItemsAction.Subscribe();
            }
            catch (System.Exception e)
            {

            }
        }


    }
}
