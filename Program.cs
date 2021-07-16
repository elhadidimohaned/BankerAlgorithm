using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {

		public void saftey(int processNum, int resourceNum, int[,] need, int[] avail, int[,] alloc, int[] safeSequence)
        {

			int count = 0;

			Boolean[] visited = new Boolean[processNum];
			for (int i = 0; i < processNum; i++)
			{
				visited[i] = false;
			}

			int[] work = new int[resourceNum];
			for (int i = 0; i < resourceNum; i++)
			{
				work[i] = avail[i];
			}

			while (count < processNum)
			{
				Boolean flag = false;
				for (int i = 0; i < processNum; i++)
				{
					if (visited[i] == false)
					{
						int j;
						for (j = 0; j < resourceNum; j++)
						{
							if (need[i, j] > work[j])
								break;
						}
						if (j == resourceNum)
						{
							safeSequence[count++] = i;
							visited[i] = true;
							flag = true;
							for (j = 0; j < resourceNum; j++)
							{
								work[j] = work[j] + alloc[i, j];
							}
						}
					}
				}
				if (flag == false)
				{
					break;
				}
			}
			if (count < processNum)
			{
				Console.WriteLine("No, This System is unsafe!");
			}
			else
			{
				Console.Write("Yes, Safe state <");
				for (int i = 0; i < processNum; i++)
				{
					Console.Write("P" + safeSequence[i]);
					if (i != processNum - 1)
						Console.Write(",");
					if (i == processNum - 1)
						Console.Write(">");
				}
			}
		}


		public void request(int processNum, int resourceNum, int[,] need, int[] avail, int[,] alloc, int[] safeSequence, int reqProcessNum)
		{

			int count = 0;

			Boolean[] visited = new Boolean[processNum];
			for (int i = 0; i < processNum; i++)
			{
				visited[i] = false;
			}

			int[] work = new int[resourceNum];
			for (int i = 0; i < resourceNum; i++)
			{
				work[i] = avail[i];
			}

			while (count < processNum)
			{
				Boolean flag = false;
				for (int i = 0; i < processNum; i++)
				{
					if (visited[i] == false)
					{
						int j;
						for (j = 0; j < resourceNum; j++)
						{
							if (need[i, j] > work[j])
								break;
						}
						if (j == resourceNum)
						{
							safeSequence[count++] = i;
							visited[i] = true;
							flag = true;
							for (j = 0; j < resourceNum; j++)
							{
								work[j] = work[j] + alloc[i, j];
							}
						}
					}
				}
				if (flag == false)
				{
					break;
				}
			}
			if (count < processNum)
			{
				Console.WriteLine("Request can't be Granted!");
			}
			else
			{
				Console.Write("Yes, request can be granted with safe state , Safe state <P" + reqProcessNum + "req,");
				for (int i = 0; i < processNum; i++)
				{
					Console.Write("P" + safeSequence[i]);
					if (i != processNum - 1)
						Console.Write(",");
					if (i == processNum - 1)
						Console.Write(">");
				}
			}
		}


		public static void Main(String[] args)
		{
			while (true)
			{

				char character = 'A';

				Console.WriteLine("Enter Number of Processes and Number of Resources seperated by spaces: ");
				var ss = Console.ReadLine();
				var datanm = ss.Split(' ');
				int processNum = Convert.ToInt32(datanm[0]);
				int resourceNum = Convert.ToInt32(datanm[1]);


				int[,] need = new int[processNum, resourceNum];
				int[] avail = new int[processNum];
				int[,] alloc = new int[processNum, resourceNum];
				int[,] max = new int[processNum, resourceNum];
				int[] safeSequence = new int[processNum];
				int[] request = new int[processNum];


				Console.WriteLine("	Allocation Matrix");
				for (int i = 0; i < processNum; i++)
				{
					Console.WriteLine("Enter P" + (i) + " resources sperated by spaces: ");
					var s = Console.ReadLine();
					var data = s.Split(' ');
					for (int j = 0; j < resourceNum; j++)
					{
						alloc[i, j] = int.Parse(data[j]);
					}
				}

				Console.WriteLine("	Max Matrix");
				for (int i = 0; i < processNum; i++)
				{
					Console.WriteLine("Enter P" + (i) + " resources sperated by spaces: ");
					var s = Console.ReadLine();
					var data = s.Split(' ');
					for (int j = 0; j < resourceNum; j++)
					{
						max[i, j] = int.Parse(data[j]); ;
					}
				}

				Console.WriteLine("Enter Available Resources Matrix sperated by spaces: ");
				var sAvail = Console.ReadLine();
				var dataAvail = sAvail.Split(' ');
				for (int i = 0; i < resourceNum; i++)
				{
					avail[i] = int.Parse(dataAvail[i]); ;
				}


				for (int i = 0; i < processNum; i++)
				{
					for (int j = 0; j < resourceNum; j++)
					{
						need[i, j] = max[i, j] - alloc[i, j];
					}
				}


				Console.WriteLine("	Need Matrix: ");
				for (int i = 0; i < processNum; i++)
				{
					if (i == 0)
					{
						Console.Write("	");
						for (int j = 0; j < resourceNum; j++)
						{
							Console.Write(Convert.ToChar(character + j) + "	");
						}
						Console.WriteLine();
					}
					Console.Write("P" + (i) + "	");
					for (int j = 0; j < resourceNum; j++)
					{
						Console.Write(need[i, j] + "	");
					}
					Console.WriteLine();
				}


				Console.WriteLine();
				Console.Write("For safe state Choose 1 and For Immediate request Choose 2: ");
				var sanswer = Console.ReadLine();
				int answer = Convert.ToInt32(sanswer);


				if (answer == 1)
				{
					Program Banker = new Program();
					Banker.saftey(processNum, resourceNum, need, avail, alloc, safeSequence);
				}

				else if (answer == 2)
				{
					Boolean granted = true;
					int[,] needNew = new int[processNum, resourceNum];
					int[] availNew = new int[processNum];
					int[,] allocNew = new int[processNum, resourceNum];

					Console.Write("Enter Process number: ");
					var sReqNum = Console.ReadLine();
					int reqProcessNum = Convert.ToInt32(sReqNum);

					Console.WriteLine("Enter Request Matrix sperated by spaces: ");
					var sReq = Console.ReadLine();
					var dataReq = sReq.Split(' ');
					for (int i = 0; i < resourceNum; i++)
					{
						request[i] = int.Parse(dataReq[i]);
					}

					for (int i = 0; i < resourceNum; i++)
					{
						if (request[i] > avail[i] || request[i] > need[reqProcessNum, i])
							granted = false; ;
					}
					if (granted)
					{

						for (int i = 0; i < resourceNum; i++)
						{
							availNew[i] = avail[i] - request[i];
						}

						for (int i = 0; i < processNum; i++)
						{
							for (int j = 0; j < resourceNum; j++)
							{
								if (i == reqProcessNum)
								{
									allocNew[i, j] = alloc[i, j] + request[j];
									needNew[i, j] = need[i, j] - request[j];

								}
								else
								{
									allocNew[i, j] = alloc[i, j];
									needNew[i, j] = need[i, j];
								}

							}
						}

						Program Banker = new Program();
						Banker.request(processNum, resourceNum, needNew, availNew, allocNew, safeSequence, reqProcessNum);


					}

				}

				else
				{
					Console.WriteLine("Wrong input!");
				}


				Console.WriteLine();

				Console.Write("Enter 1 if you want to continue, else enter 0: ");
				var scont = Console.ReadLine();
				int cont = Convert.ToInt32(scont);
				if (cont == 0)
					break;
			}
		}

	}

}