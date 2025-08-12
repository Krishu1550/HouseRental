import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerPanel } from './seller-panel';

describe('SellerPanel', () => {
  let component: SellerPanel;
  let fixture: ComponentFixture<SellerPanel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SellerPanel]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SellerPanel);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
