export interface Balance {
    balance: number;
    balanceIncludedCostPlans: number;
    currencyCode: string;
    currencySymbol: string;
    incomeCategoryShares: CategoryShare[];
    expenseCategoryShares: CategoryShare[];
    monthlyCosts: MonthlyCost[];
  }
  
  interface CategoryShare {
    categoryName: string;
    percentage: number;
  }
  
  interface MonthlyCost {
    month: number;
    year: number;
    totalCost: number;
  }