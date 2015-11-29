namespace WorldSalt.Server.RefStub {
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using WorldSalt.Server.RefStub.Connections;

	public class ServerProcess {
		private IConnectionFoyer foyer;
		private IList<Task> clients;

		public ServerProcess(IConnectionFoyer foyer) {
			this.foyer = foyer;
			clients = new List<Task>();
		}

		public void AcceptOne() {
			RunClientHandlerInNewTask(foyer.AcceptOne());
			CleanUpDisconnectedClients();
		}

		private void RunClientHandlerInNewTask(IClientHandler clientHandler) {
			clients.Add(Task.Factory.StartNew(clientHandler.Run));
		}

		private void CleanUpDisconnectedClients() {
			IList<int> tombstones = new List<int>();
			foreach (var i in Enumerable.Range(0, clients.Count).Reverse()) {
				var task = clients[i];
				if(task.IsCompleted || task.IsCanceled || task.IsFaulted) {
					tombstones.Add(i);
				}
			}

			foreach(var i in tombstones) {
				clients.RemoveAt(i);
			}
		}
	}
}
