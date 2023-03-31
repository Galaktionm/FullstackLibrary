import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookdisplayComponent } from './bookdisplay.component';

describe('BookdisplayComponent', () => {
  let component: BookdisplayComponent;
  let fixture: ComponentFixture<BookdisplayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BookdisplayComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookdisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
