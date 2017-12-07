import {AfterViewInit, Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ContactsService} from "../../../services/contacts.service";


@Component({
  selector: 'app-contact-modal',
  templateUrl: './contact-modal.component.html',
  styleUrls: []
})
export class ContactModalComponent implements OnInit, AfterViewInit {
  @Input() temp_contact: any;
  @Output() onClose: EventEmitter<any> = new EventEmitter();

  constructor(private contactsService: ContactsService) {

  }

  ngAfterViewInit() {
  }

  ngOnInit() {

  }

  updateContact() {
    this.contactsService.editContact(this.temp_contact).subscribe((contact: any) => {
      this.onClose.next(this.temp_contact); // emit event
    });
  }


}
