import {AfterViewInit, Component, OnInit} from '@angular/core';
import {each} from 'lodash';
import * as moment from 'moment';
import {Logger} from '../../core/services/logger.service';
import {ContactsService} from "../../services/contacts.service";
declare let $: any;

const logger = new Logger('contacts');
@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: []
})
export class ContactsComponent implements OnInit {
  contacts: any[];

  constructor(private contactsService: ContactsService) {
    setTimeout(function () {
      $(function () {
        $('#datatable').dataTable();
      });
    }, 400);

  }

  ngOnInit() {
    const _this = this;
    this.contactsService.getContacts().subscribe((contacts: any) => {
      _this.contacts = contacts;
    });

  }

  addContact() {
    console.log('hamooo');
  }

}
