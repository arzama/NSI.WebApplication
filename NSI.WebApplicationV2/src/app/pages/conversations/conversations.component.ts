import 'rxjs/add/operator/map';
import { Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { IConversation } from "./Models/conversations";
import { ConversationService } from "../../services/conversations.service";
import { SlicePipe } from '@angular/common';
import { IMessage } from "./Models/message";
import { HubConversationService } from "../../services/hub.conversation.service";
import { IParticipant } from "./Models/participant";
import { ActivatedRoute } from '@angular/router';
import { IUser } from "./Models/user";
import { HubConnection } from "@aspnet/signalr-client/dist/src";
import { environment } from "../../../environments/environment";


@Component({
    selector: 'app-conversations',
    templateUrl: './conversations.component.html',
    styleUrls: ['./conversations.component.scss']
})
export class ConversationsComponent implements OnInit{
    

    public async: any;
    public messageListSelectedConversation : IMessage[] = [];
    public loggedUserId : number;
    public _conversations: IConversation[] = [];
    public _participants: IParticipant[];
    public newMessage: string;
    public messageModel:IMessage; 
    public activeConversationId: number;
    public participantForMessageModel: IParticipant;
    //Fields for establishing connection on hub
    private _url: string;
    private _hubConnection: HubConnection;

    constructor(private _conversationService: ConversationService,  private route: ActivatedRoute) {
        this._url = environment.serverUrl;
    }

   
    public loadMessages( id: number): void{
        this.messageListSelectedConversation = this._conversations.filter((conversation: IConversation) => conversation.conversationId == id)[0].message
        this.activeConversationId = id;
        this._conversationService.getParticipants(id)
        .subscribe(
            participants => {
                this._participants =  participants;
            }
        );
    }

    public isMessageSender(loggedUserId: number, senderUserId: number): boolean{
        return loggedUserId == senderUserId;
    }

    public sendMessage():void{
        
        let participant = this.getParticipanWithUserId(this.loggedUserId);
        //this._hubConversationService.sendMessage(this.newMessage,this.messageListSelectedConversation[0].conversationId,this.loggedUserId);
        this._hubConnection.invoke('Send',this.newMessage,this.messageListSelectedConversation[0].conversationId,this.loggedUserId, participant.participantId);
        //Create message model to refresh messages in current chat window
        //this.messageListSelectedConversation.push(this.messageModel);
    }


    private getUserFromParticipants(id:number): IUser {
       let participant = this._participants.find(x => x.user.id == id);
       return participant.user;  
    }

    private getParticipanWithUserId(id: number): IParticipant{
        let participant = this._participants.find(x => x.user.id == id);
        return participant;
    }

    private createMessageModel(message: string, activeConversationId: number, loggedUserId: number, participant: IParticipant ): void
    {
        this.messageModel = {};
        //This is temporary Id
        this.messageModel.messageId = 0;//this.messageListSelectedConversation[this.messageListSelectedConversation.length - 1].messageId + 1;
        this.messageModel.message = message;
        this.messageModel.createdByUser =participant.user;
        this.messageModel.dateCreated = Date.now().toString();
        this.messageModel.conversationId = activeConversationId;
       
    } 

   

    ngOnInit() {

        this.route.params.subscribe(params => {
            this.loggedUserId = +params['id']; 
         });
        
         
        this._conversationService.getConversations(this.loggedUserId)
        .subscribe(
            conversations => {
                this._conversations = conversations;
            }
        )

        this._hubConnection = new HubConnection(`${this._url}/chat`);
        
        this._hubConnection.on('Send', (data, activeConversationId, loggedUserId, participantId) => {
            

              //this._conversationService.getParticipantById(participantId)
              //.subscribe(
                  //participant => {

                      this.participantForMessageModel = this.getParticipanWithUserId(loggedUserId);
                      this.createMessageModel(data,activeConversationId,loggedUserId,this.participantForMessageModel);
                      this.messageListSelectedConversation.push(this.messageModel);
                  //}
              //)
                         
          });
                
                this._hubConnection.start()
                    .then(() => {
                        console.log('Hub connection started')
                    })
                    .catch(err => {
                        console.log('Error while establishing connection')
                    });

                
               

                    
                

    }
}
