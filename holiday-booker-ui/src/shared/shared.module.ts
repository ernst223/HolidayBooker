import { LayoutModule } from '@angular/cdk/layout';
import { CommonModule } from '@angular/common';
import { NgModule  } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MaterialComponentsModule } from './material-components.module';
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { MatBottomSheetModule, MatChipsModule, MatGridListModule } from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
    imports: [
        CommonModule,
        MaterialComponentsModule,
        FlexLayoutModule,
        MatBottomSheetModule,
        MatChipsModule,
        HttpClientModule,
        ReactiveFormsModule,
        FormsModule
    ],
    exports: [
        CommonModule,
        MaterialComponentsModule,
        FlexLayoutModule,
        MatBottomSheetModule,
        MatChipsModule,
    ],
    declarations: [

    ],
    entryComponents: [

    ],
    providers: [{ provide: MAT_DATE_LOCALE, useValue: 'en-ZA' },
    ],
})
export class SharedModule { }
