// This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
// To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/ 
// or send a letter to Creative Commons, PO Box 1866, Mountain View, CA 94042, USA.
using System;
using System.Collections; 
using System.Collections.Generic; 
using System.Net; 
using System.Net.Sockets; 
using System.Text; 
using System.Threading; 
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Wifi{
	public class tcp2 : MonoBehaviour {  	
	#region private members 	
	/// <summary> 	
	/// TCPListener to listen for incomming TCP connection 	
	/// requests. 	
	/// </summary> 	
	private TcpListener tcpListener; 
	/// <summary> 
	/// Background thread for TcpServer workload. 	
	/// </summary> 	
	private Thread tcpListenerThread;  	
	/// <summary> 	
	/// Create handle to connected tcp client. 	
	/// </summary> 	
	private TcpClient connectedTcpClient; 	
	#endregion 	

	public float HanadValue,LegValue;
	public string IP ;

	// public bool listen;
	// 開機

	private void Start()
	{
		DontDestroyOnLoad(gameObject);
		tcpListenerThread = new Thread (new ThreadStart(ListenForIncommingRequests)); 		
		tcpListenerThread.IsBackground = true; 		
		tcpListenerThread.Start(); 
		
	}


	/// <summary> 	
	/// Runs in background TcpServerThread; Handles incomming TcpClient requests 	
	/// </summary> 	
	private void ListenForIncommingRequests () { 		
		try { 			
			// 建立閘道為8052的伺服器 			
			tcpListener = new TcpListener(IPAddress.Parse(IP), 8052); 			
			tcpListener.Start();              
			Debug.Log("Server is listening");              
			Byte[] bytes = new Byte[1024];  	

			// 接收訊息
			while (true) {
				using (connectedTcpClient = tcpListener.AcceptTcpClient()) { 					
					// Get a stream object for reading 					
					using (NetworkStream stream = connectedTcpClient.GetStream()) { 						
						int length; 						
						// Read incomming stream into byte arrary. 						
						while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) { 							
							var incommingData = new byte[length]; 							
							Array.Copy(bytes, 0, incommingData, 0, length);  							
							// Convert byte array to string message. 							
							string clientMessage = Encoding.ASCII.GetString(incommingData);
							// Debug.Log("client message received as: " + clientMessage);
							Classification(clientMessage);
						} 					
					} 				
				}
			} 		
		} 		
		catch (SocketException socketException) {
			Debug.Log("SocketException " + socketException.ToString()); 		
		}     
	}  	

	void Classification(string msg){
		string[] Smsg = msg.Split(" ");
		if(Smsg[0] == "H" && float.Parse(Smsg[1])>HanadValue){
			HanadValue = float.Parse(Smsg[1]);
		}
	}

	public void clearHandValue(){
		HanadValue = 0;
	}	
	// 關機
	 private void OnDisable()
    {
	    closeS();
    }

	 public void closeS()
	 {
		 Debug.Log("begin OnDisable()");
		 try
		 {
			 tcpListenerThread.Interrupt(); 
		 }
		 catch (Exception e)
		 {
			 Debug.Log(e.Message);
		 }
		 Debug.Log("end OnDisable()");
	 }
	

}
}
