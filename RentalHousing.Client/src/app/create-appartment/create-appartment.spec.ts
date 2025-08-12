import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateAppartment } from './create-appartment';

describe('CreateAppartment', () => {
  let component: CreateAppartment;
  let fixture: ComponentFixture<CreateAppartment>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateAppartment]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateAppartment);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
