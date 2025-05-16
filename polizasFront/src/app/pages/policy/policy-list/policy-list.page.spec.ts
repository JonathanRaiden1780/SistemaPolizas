import { ComponentFixture, TestBed } from "@angular/core/testing";

import { PolicyListPage } from "./policy-list.page";

describe("PolicyListPage", () => {
  let component: PolicyListPage;
  let fixture: ComponentFixture<PolicyListPage>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PolicyListPage],
    }).compileComponents();

    fixture = TestBed.createComponent(PolicyListPage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
