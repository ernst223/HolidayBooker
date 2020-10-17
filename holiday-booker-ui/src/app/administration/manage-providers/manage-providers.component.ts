import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatSort, MatPaginator, MatSnackBar } from '@angular/material';
import { Supplier } from 'src/shared/shared.models';
import { SharedService } from 'src/shared/shared.serice';

@Component({
  selector: 'app-manage-providers',
  templateUrl: './manage-providers.component.html',
  styleUrls: ['./manage-providers.component.scss']
})
export class ManageProvidersComponent implements OnInit {
  @ViewChild(MatSort, {static: false}) sort: MatSort;
  @ViewChild(MatPaginator, {static: false}) paginator: MatPaginator;
  DescriptionValue: string;
  CodeValue: string;
  EmailValue: string;
  ContactValue: string;
  Data: Supplier[];
  orderDataSource: MatTableDataSource<object> = new MatTableDataSource();
  displayedColumns = ['Code', 'Description', 'Email', 'Contact', 'Actions'];
  EditableId: Number;
  EditCodeValue: string;
  EditDescriptionValue: string;
  EditEmailValue: string;
  EditContactValue: string;
  selectedRowIndex: number = -1;

  constructor(private snackBar: MatSnackBar, private service: SharedService) { }

  ngOnInit() {
    this.setupDataStream();
  }

  ngAfterViewInit() {
    this.orderDataSource.sort = this.sort;
    this.orderDataSource.paginator = this.paginator;
  }

  edit(id: any, editcode: any, editdescription: any, editemail: any, editcontact: any) {
    this.EditableId = id;
    this.EditCodeValue = editcode;
    this.EditDescriptionValue = editdescription;
    this.EditEmailValue = editemail;
    this.EditContactValue = editcontact;
    this.selectedRowIndex = id;
  }
  ChangeEditMe() {
    this.EditableId = null;
    this.selectedRowIndex = -1;
  }
  SaveEdit(id: any) {
    const data: Supplier = {
      Id: id,
      Code: this.EditCodeValue,
      Description: this.EditDescriptionValue,
      Email: this.EditEmailValue,
      Contact: this.EditContactValue
    };
    this.service.editSupplier(data).subscribe(a => {
      this.openSnackBar('Update Successful', '');
      this.EditCodeValue = '';
      this.EditDescriptionValue = '';
      this.EditEmailValue = '';
      this.EditContactValue = '';
      this.ChangeEditMe();
      this.selectedRowIndex = -1;
    });
  }
  testEditabliity(id: any): boolean {
    if (id == this.EditableId) {
      return true;
    } else {
      return false;
    }
  }

  delete(id: any) {
    this.service.deleteSupplier(id).subscribe(a => {
      console.log('Entity deleted');
      this.openSnackBar('Deleted', '');
    });
  }
  add() {
    const data: Supplier = {
      Id: 0,
      Code: this.CodeValue,
      Email: this.EmailValue,
      Contact: this.ContactValue,
      Description: this.DescriptionValue
    };

    this.service.addSupplier(data).subscribe(a => {
      this.openSnackBar('Successful', '');
      this.DescriptionValue = '';
      this.CodeValue = '';
      this.EmailValue = '';
      this.ContactValue = '';
    });
  }
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.orderDataSource.filter = filterValue;
  }

  // Loading the data from server
  loadData() {
    return this.service.getSuppliers();
  }
   setupDataStream() {
     this.loadData().subscribe(a => {
       this.Data = a;
       this.orderDataSource.data = this.Data;
     });
   }

   openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
    this.setupDataStream();
  }
}
