import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TenantPanel } from './tenant-panel';

describe('TenantPanel', () => {
  let component: TenantPanel;
  let fixture: ComponentFixture<TenantPanel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TenantPanel]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TenantPanel);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
